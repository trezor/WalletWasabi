using NBitcoin;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using WalletWasabi.WabiSabi.Backend.Rounds;
using WalletWasabi.WabiSabi.Models;

namespace WalletWasabi.WabiSabiClientLibrary.Models.SelectUtxoForRound;

public record Constants(
	MoneyRange AllowedInputAmounts,
	MoneyRange AllowedOutputAmounts,
	ImmutableSortedSet<ScriptType> AllowedInputTypes,
	CoordinationFeeRate CoordinationFeeRate,
	FeeRate MiningFeeRate
) : IUtxoSelectionParameters;
