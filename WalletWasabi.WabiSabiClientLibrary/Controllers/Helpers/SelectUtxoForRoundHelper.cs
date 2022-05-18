using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using WalletWasabi.Blockchain.TransactionOutputs;
using WalletWasabi.Crypto.Randomness;
using WalletWasabi.WabiSabi.Client;
using WalletWasabi.WabiSabiClientLibrary.Models;

namespace WalletWasabi.WabiSabiClientLibrary.Controllers.Helpers;

public class SelectUtxoForRoundHelper
{
	public static SelectUtxoForRoundResponse Select(SelectUtxoForRoundRequest request, WasabiRandom? rnd = null)
	{
		rnd ??= SecureRandom.Instance;
		ImmutableList<ISmartCoin> coins = CoinJoinClient.SelectCoinsForRound<ISmartCoin>(request.Utxos, request.Constants, request.ConsolidationMode, request.AnonScoreTarget, rnd);

		Dictionary<ISmartCoin, int> coinIndices = request.Utxos
			.Select((x, i) => ((ISmartCoin)x, i))
			.ToDictionary(x => x.Item1, x => x.Item2);

		// Find corresponding indices for the found coins.
		int[] indices = coins.Select(c => coinIndices[c]).ToArray();

		return new SelectUtxoForRoundResponse(indices);
	}
}
