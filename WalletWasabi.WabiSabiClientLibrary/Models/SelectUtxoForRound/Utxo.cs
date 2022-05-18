using NBitcoin;
using WalletWasabi.Blockchain.TransactionOutputs;

namespace WalletWasabi.WabiSabiClientLibrary.Models.SelectUtxoForRound;

/// <summary>
/// Represents a single UTXO.
/// </summary>
/// <param name="Value">Value of the UTXO in Bitcoin (not effective value).</param>
/// <param name="ScriptType">UTXO's script type.</param>
/// <param name="AnonymitySet">Anonymity set assigned to the UTXO.</param>
/// <param name="LastCoinjoinTimestamp">The parameter is not currently needed in seconds.</param>
public record Utxo(string TxId, uint Index, Money Value, ScriptType ScriptType, int AnonymitySet, long LastCoinjoinTimestamp) : ISmartCoin
{
	public Money Amount => Value;

	public uint256 TransactionId { get; } = new(TxId);
}
