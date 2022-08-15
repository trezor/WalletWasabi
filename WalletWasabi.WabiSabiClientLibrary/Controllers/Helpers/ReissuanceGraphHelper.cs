using NBitcoin;
using System.Collections.Generic;
using System.Linq;
using WalletWasabi.WabiSabi.Client.CredentialDependencies;
using WalletWasabi.WabiSabiClientLibrary.Models;
using WalletWasabi.WabiSabiClientLibrary.Models.DependencyGraph;
using InputNode = WalletWasabi.WabiSabiClientLibrary.Models.DependencyGraph.InputNode;
using OutputNode = WalletWasabi.WabiSabiClientLibrary.Models.DependencyGraph.OutputNode;

namespace WalletWasabi.WabiSabiClientLibrary.Controllers.Helpers;

public class ReissuanceGraphHelper
{
	public static GetReissuanceGraphResponse GetGraph(GetReissuanceGraphRequest request)
	{
		IReadOnlyList<(Money effectiveValue, int vsize)> inputEffectiveValuesAndSizes = Convert(request.InputValues);
		IReadOnlyList<(Money effectiveValue, int vsize)> outputEffectiveValuesAndSizes = Convert(request.OutputValues);

		DependencyGraph dependencyGraph = DependencyGraph.ResolveCredentialDependencies(inputEffectiveValuesAndSizes, outputEffectiveValuesAndSizes, request.Constants.MaxVsizeAllocationPerAlice);

		List<InputNode> inputNodes = new(capacity: inputEffectiveValuesAndSizes.Count);

		for (int i = 0; i < inputEffectiveValuesAndSizes.Count; i++)
		{
			inputNodes.Add(new InputNode(Id: dependencyGraph.Inputs[i].Id, InputIndex: i));
		}

		List<OutputNode> outputNodes = new(capacity: outputEffectiveValuesAndSizes.Count);

		for (int i = 0; i < outputEffectiveValuesAndSizes.Count; i++)
		{
			outputNodes.Add(new OutputNode(Id: dependencyGraph.Outputs[i].Id, OutputIndex: i));
		}

		List<InnerNode> innerNodes = new(capacity: dependencyGraph.Reissuances.Count);

		for (int i = 0; i < dependencyGraph.Reissuances.Count; i++)
		{
			innerNodes.Add(new InnerNode(Id: dependencyGraph.Reissuances[i].Id, Value: dependencyGraph.Reissuances[i].InitialBalance(CredentialType.Amount)));
		}

		CredentialNodes credentialNodes = new(inputNodes.ToArray(), outputNodes.ToArray(), innerNodes.ToArray());

		ReissuanceGraph reissuanceGraph = new(credentialNodes, null! /* TODO */);

		return new GetReissuanceGraphResponse(reissuanceGraph);
	}

	/// <summary>
	/// Helper function to convert to the tuple form.
	/// </summary>
	private static IReadOnlyList<(Money effectiveValue, int vsize)> Convert(Values values)
	{
		IEnumerable<Money> amounts = values.Amount.Select(x => Money.Satoshis(x));
		return amounts.Zip(values.Vsize).ToArray();
	}
}
