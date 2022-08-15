using NBitcoin;
using WalletWasabi.WabiSabi.Models;

namespace WalletWasabi.WabiSabiClientLibrary.Models.DependencyGraph;

public record Constants(
	CoordinationFeeRate CoordinationFee, // TODO: Should it be called "CoordinationFeeRate"?
	FeeRate FeeRate,
	long MaxVsizeAllocationPerAlice
);
