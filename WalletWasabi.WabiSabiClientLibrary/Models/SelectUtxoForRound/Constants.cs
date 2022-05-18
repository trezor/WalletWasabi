using NBitcoin;
using System.Collections.Generic;
using System.Collections.Immutable;
using WalletWasabi.WabiSabi.Backend.Rounds;
using WalletWasabi.WabiSabi.Models;

namespace WalletWasabi.WabiSabiClientLibrary.Models.SelectUtxoForRound;

public record Constants(
	MoneyRange AllowedInputAmounts,
	MoneyRange AllowedOutputAmounts,
	IReadOnlyList<string[]> AllowedInputTypes,
	CoordinationFeeRate CoordinationFeeRate,
	FeeRate MiningFeeRate
) : IUtxoSelectionParameters
{
	public ImmutableSortedSet<ScriptType> AllowedInputScriptTypes { get; init; } = ImmutableSortedSet.Create(ScriptType.P2WPKH);
}
