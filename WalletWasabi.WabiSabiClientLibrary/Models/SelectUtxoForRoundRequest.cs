using System.ComponentModel;
using NBitcoin;
using Newtonsoft.Json;
using WalletWasabi.WabiSabi.Backend.Rounds;
using WalletWasabi.WabiSabiClientLibrary.Models.SelectUtxoForRound;

namespace WalletWasabi.WabiSabiClientLibrary.Models;

/// <summary>
/// Represents a set of available UTXOs and various parameters to select a set of UTXOs satisfying all conditions for a CoinJoin round.
/// </summary>
/// <param name="Utxos">Set of the UTXOs to choose from.</param>
/// <param name="AnonScoreTarget">Required anonymity score target.</param>
/// <param name="Constants">Parameters affecting UTXOs selection.</param>
/// <param name="ConsolidationMode">This option will likely be removed. We expect value to be <c>false</c>.</param>
public record SelectUtxoForRoundRequest(
	Utxo[] Utxos,
	int AnonScoreTarget,
	UtxoSelectionParameters Constants,
	int SemiPrivateThreshold,
	[property: DefaultValue(0), JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)] Money LiquidityClue,
	bool ConsolidationMode = false
);
