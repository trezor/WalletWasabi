namespace WalletWasabi.WabiSabiClientLibrary.Models;

/// <summary>
/// Response object for <see cref="SelectUtxoForRoundRequest"/>.
/// </summary>
/// <param name="Indices">Indices of the selected UTXOs from the corresponding <see cref="SelectUtxoForRoundRequest.Utxos"/> array.</param>
public record SelectUtxoForRoundResponse(
	int[] Indices
);
