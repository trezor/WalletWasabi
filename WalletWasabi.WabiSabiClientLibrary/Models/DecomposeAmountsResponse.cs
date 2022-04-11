namespace WalletWasabi.WabiSabiClientLibrary.Models;

/// <summary>
/// Response object for <see cref="DecomposeAmountsRequest"/>.
/// </summary>
/// <param name="OutputAmounts">Output amounts in satoshis.</param>
public record DecomposeAmountsResponse(
	long[] OutputAmounts
);
