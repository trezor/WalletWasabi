using WalletWasabi.Helpers;
using Xunit;
using WalletWasabi.Helpers;

namespace WalletWasabi.Tests.UnitTests;

public class ConstantsTests
{
	[Fact]
	public void True()
	{
		Assert.Equal(Constants.P2wpkhInputMaximumVirtualSize, 68);
		Assert.Equal(Constants.P2wpkhOutputVirtualSize, 31);
		Assert.Equal(Constants.P2trInputMaximumVirtualSize, 58);
		Assert.Equal(Constants.P2trOutputVirtualSize, 43);
	}
}
