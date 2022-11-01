using System.Collections.Generic;
using System.Linq;
using NBitcoin;
using NBitcoin.Crypto;
using WalletWasabi.Blockchain.Analysis;
using WalletWasabi.Blockchain.Analysis.Clustering;
using WalletWasabi.Blockchain.Keys;
using WalletWasabi.Blockchain.TransactionOutputs;
using WalletWasabi.Blockchain.Transactions;
using WalletWasabi.Extensions;
using WalletWasabi.Helpers;
using WalletWasabi.WabiSabiClientLibrary.Models;
using WalletWasabi.WabiSabiClientLibrary.Models.AnalyzeTransactions;

namespace WalletWasabi.WabiSabiClientLibrary.Controllers.Helpers;

public class AnalyzeTransactionsHelper
{
	public static AnalyzeTransactionsResponse AnalyzeTransactions(AnalyzeTransactionsRequest request)
	{
		TransactionLabelProvider labelProvider = new();
		BlockchainAnalyzer analyser = new(0);

		HashSet<AnalyzedTransaction> toAnalyzeTransactions = request.Transactions.Select(x => AnalyzedTransaction.FromTransaction(x, labelProvider)).ToHashSet();
		HashSet<AnalyzedTransaction> analyzedTransactions = new();

		// Analyze transactions in topological sorting
		void AnalyzeRecursively(AnalyzedTransaction transaction)
		{
			if (!analyzedTransactions.Contains(transaction))
			{
				analyzedTransactions.Add(transaction);
				foreach (TransactionLabel label in transaction.InputTransactionLabels)
				{
					IEnumerable<AnalyzedTransaction>? previousTransactions = toAnalyzeTransactions.Where(x => x.OutputTransactionLabels.Contains(label));
					if (previousTransactions.Count() == 0)
					{
						throw new Exception("Invalid input: There is an internal input that references a non-existing transaction.");
					}
					foreach (var previousTransaction in previousTransactions)
					{
						AnalyzeRecursively(previousTransaction);
					}
				}
				analyser.Analyze(transaction);
			}
		}

		foreach (var transaction in toAnalyzeTransactions)
		{
			AnalyzeRecursively(transaction);
		}

		return new AnalyzeTransactionsResponse(labelProvider.GetAnonymitySets().ToArray());
	}
	private class TransactionLabelProvider
	{
		private Dictionary<string, TransactionLabel> AddressToTransactionLabel { get; }

		public TransactionLabelProvider()
		{
			AddressToTransactionLabel = new();
		}

		public IEnumerable<AddressAnonymity> GetAnonymitySets()
		{
			return AddressToTransactionLabel.Select(x => new AddressAnonymity(x.Key, x.Value.HdPubKey.AnonymitySet));
		}

		public TransactionLabel GetTransactionLabel(string address)
		{
			if (!AddressToTransactionLabel.ContainsKey(address))
			{
				byte[] keyId = NBitcoinHelpers.BetterParseBitcoinAddress(address).ScriptPubKey.ToBytes();
				Key dummyKey = new(Hashes.SHA256(keyId));
				HdPubKey dummyHdPubKey = new(dummyKey.PubKey, new KeyPath("0/0/0/0/0"), SmartLabel.Empty, KeyState.Clean);
				AddressToTransactionLabel[address] = new TransactionLabel(dummyHdPubKey);
			}

			return AddressToTransactionLabel[address];
		}
	}

	private class TransactionLabel
	{
		public HdPubKey HdPubKey { get; }

		public TransactionLabel(HdPubKey hdPubKey)
		{
			HdPubKey = hdPubKey;
		}
	}

	private class AnalyzedTransaction : SmartTransaction
	{
		static private Network Network { get; } = Network.Main;

		public List<TransactionLabel> InputTransactionLabels { get; }
		public List<TransactionLabel> OutputTransactionLabels { get; }

		public AnalyzedTransaction()
			// Network is not used in Transaction.Create
			: base(NBitcoin.Transaction.Create(Network), 0)
		{
			InputTransactionLabels = new List<TransactionLabel>();
			OutputTransactionLabels = new List<TransactionLabel>();
		}

		public static AnalyzedTransaction FromTransaction(Tx transaction, TransactionLabelProvider transactionLabelProvider)
		{
			AnalyzedTransaction analyzedTransaction = new();

			foreach (var internalInput in transaction.InternalInputs)
			{
				analyzedTransaction.AddInternalInput(internalInput.Value, transactionLabelProvider.GetTransactionLabel(internalInput.Address));
			}

			foreach (var externalInput in transaction.ExternalInputs)
			{
				analyzedTransaction.AddExternalInput();
			}

			foreach (var internalOutput in transaction.InternalOutputs)
			{
				analyzedTransaction.AddInternalOutput(internalOutput.Value, transactionLabelProvider.GetTransactionLabel(internalOutput.Address));
			}

			foreach (var externalOutput in transaction.ExternalOutputs)
			{
				analyzedTransaction.AddExternalOutput(externalOutput.Value, externalOutput.ScriptPubKey);
			}

			return analyzedTransaction;
		}

		public void AddExternalInput()
		{
			Transaction.Inputs.Add(new OutPoint());
		}

		public void AddExternalOutput(long amountInSatoshis, string scriptPubKey)
		{
			Script script = Script.FromHex(scriptPubKey);
			Transaction.Outputs.Add(new Money(amountInSatoshis, MoneyUnit.Satoshi), script);
		}

		public void AddInternalInput(long amountInSatoshis, TransactionLabel label)
		{
			InputTransactionLabels.Add(label);

			NBitcoin.Transaction previousTransaction = NBitcoin.Transaction.Create(Network);
			previousTransaction.Outputs.Add(new Money(amountInSatoshis), label.HdPubKey.PubKey.GetScriptPubKey(ScriptPubKeyType.TaprootBIP86));
			SmartTransaction previousSmartTransaction = new SmartTransaction(previousTransaction, 0);

			Transaction.Inputs.Add(new OutPoint());
			SmartCoin smartCoin = new(previousSmartTransaction, 0, label.HdPubKey);
			WalletInputs.Add(smartCoin);
		}

		public void AddInternalOutput(long amountInSatoshis, TransactionLabel label)
		{
			OutputTransactionLabels.Add(label);

			Transaction.Outputs.Add(new Money(amountInSatoshis, MoneyUnit.Satoshi), label.HdPubKey.PubKey.GetScriptPubKey(ScriptPubKeyType.TaprootBIP86));
			SmartCoin smartCoin = new(this, (uint)Transaction.Outputs.Count - 1, label.HdPubKey);
			WalletOutputs.Add(smartCoin);
		}
	}

}

