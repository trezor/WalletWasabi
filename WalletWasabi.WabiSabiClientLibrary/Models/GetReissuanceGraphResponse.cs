using WalletWasabi.WabiSabiClientLibrary.Models.DependencyGraph;

namespace WalletWasabi.WabiSabiClientLibrary.Models;

/// <summary>
/// Response object for <see cref="GetReissuanceGraphRequest"/>.
/// </summary>
/// <param name="ReissuanceGraph">Output amounts in satoshis.</param>
public record GetReissuanceGraphResponse(ReissuanceGraph ReissuanceGraph);
