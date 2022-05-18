using NBitcoin;
using Newtonsoft.Json;
using WalletWasabi.Blockchain.TransactionOutputs;

namespace WalletWasabi.WabiSabiClientLibrary.Models.SelectInputsForRound;

/// <summary>
/// Represents a single UTXO.
/// </summary>
/// <param name="Outpoint">The outpoint identififying the UTXO</param>
/// <param name="Amount">Value of the UTXO in Bitcoin (not effective value).</param>
/// <param name="ScriptType">UTXO's script type.</param>
/// <param name="AnonymitySet">Anonymity set assigned to the UTXO.</param>
public record Utxo(OutPoint Outpoint, Money Amount, ScriptType ScriptType, double AnonymitySet) : ISmartCoin
{
	[JsonIgnore]
	public uint Index => Outpoint.N;

	[JsonIgnore]
	public uint256 TransactionId => Outpoint.Hash;
}
