using WalletWasabi.Helpers;
using Xunit;

namespace WalletWasabi.Tests.UnitTests;

public class ConstantsTests
{
	[Fact]
	public void True()
	{
		Assert.Equal(68, Constants.P2wpkhInputMaximumVirtualSize);
		Assert.Equal(31, Constants.P2wpkhOutputVirtualSize);
		Assert.Equal(58, Constants.P2trInputMaximumVirtualSize);
		Assert.Equal(43, Constants.P2trOutputVirtualSize);
	}
}
