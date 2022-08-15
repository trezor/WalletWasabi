using System.Linq;

namespace WalletWasabi.WabiSabi.Client.CredentialDependencies;

public class ReissuanceNode : RequestNode
{
	public ReissuanceNode(int id) :
		base(id,
			Enumerable.Repeat(0L, DependencyGraph.CredentialTypes.Length),
			DependencyGraph.K,
			DependencyGraph.K,
			DependencyGraph.K * (DependencyGraph.K - 1))
	{
	}
}
