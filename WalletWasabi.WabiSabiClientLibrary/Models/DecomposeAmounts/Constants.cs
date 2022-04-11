using NBitcoin;
using WalletWasabi.WabiSabi.Models;

namespace WalletWasabi.WabiSabiClientLibrary.Models.DecomposeAmounts;

public record Constants(
	FeeRate FeeRate,
	MoneyRange AllowedOutputAmounts
);
