using NBitcoin;
using System.Collections.Generic;

namespace WalletWasabi.WabiSabi.Client.CredentialDependencies;

public class InputNode : RequestNode
{
	public InputNode(int id, IEnumerable<long> values) : base(id, values, inDegree: 0, outDegree: DependencyGraph.K, zeroOnlyOutDegree: DependencyGraph.K * (DependencyGraph.K - 1))
	{
	}

	public Money EffectiveValue => Money.Satoshis(InitialBalance(CredentialType.Amount));

	public int VsizeRemainingAllocation => (int)InitialBalance(CredentialType.Vsize);
}
