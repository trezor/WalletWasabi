using NBitcoin;
using WalletWasabi.WabiSabi.Models;

namespace WalletWasabi.WabiSabiClientLibrary.Models.SelectUtxoForRound;

public record CoordinationFee(
	FeeRate Rate,
	MoneyRange PlebsDontPayThreshold
);
