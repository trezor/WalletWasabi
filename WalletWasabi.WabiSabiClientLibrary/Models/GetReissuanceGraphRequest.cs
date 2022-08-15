using WalletWasabi.WabiSabiClientLibrary.Models.DependencyGraph;

namespace WalletWasabi.WabiSabiClientLibrary.Models;

/// <summary>
/// Given values of credentials received in the input registration and the connection confirmation
/// phases and values needed in the output registration phase, construct a graph that represent
/// a way in which input values can be converted to the output values.
/// </summary>
/// <param name="InputValues">Input registration values.</param>
/// <param name="OutputValues">Output registration values.</param>
/// <param name="Constants">Additional parameters.</param>
public record GetReissuanceGraphRequest(
	Values InputValues,
	Values OutputValues,
	Constants Constants
);
