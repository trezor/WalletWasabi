using WalletWasabi.WabiSabiClientLibrary.Models.DecomposeAmounts;

namespace WalletWasabi.WabiSabiClientLibrary.Models;

/// <summary>
/// Represents a set of effective input amounts registered by a participant and a set of effective input amounts
/// registered by other participants to decompose into output amounts.
/// </summary>
/// <param name="InternalAmounts">Effective inputs amounts registered by a participant.</param>
/// <param name="ExternalAmounts">Effective inputs amounts registered by other participants.</param>
/// <param name="OutputSize">Virtual size of an output.</param>
/// <param name="AvailableVsize">Available virtual size.</param>
/// <param name="Constants"></param>
public record DecomposeAmountsRequest(
	decimal[] InternalAmounts,
	decimal[] ExternalAmounts,
	int InputSize,
	int OutputSize,
	int AvailableVsize,
	Constants Constants
);
