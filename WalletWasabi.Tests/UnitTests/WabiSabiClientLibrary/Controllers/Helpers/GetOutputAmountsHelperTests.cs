using NBitcoin;
using WalletWasabi.WabiSabiClientLibrary.Controllers.Helpers;
using WalletWasabi.WabiSabiClientLibrary.Models;
using WalletWasabi.WabiSabi.Models;
using Xunit;

namespace WalletWasabi.Tests.UnitTests.WabiSabiClientLibrary.Controllers.Helpers;

public class GetOutputAmountsHelperTests
{
	[Fact]
	public void SimpleGetOutputAmountsTest()
	{
		GetOutputAmountsRequest request = new(
			InternalAmounts: new decimal[] { 1000m },
			ExternalAmounts: new decimal[] { 2000m },
			OutputSize: 50,
			InputSize: 58,
			AvailableVsize: 10_000,
			MiningFeeRate: new FeeRate(100L),
			AllowedOutputAmounts: new MoneyRange(Min: 10L, Max: 10_000L)
		);

		GetOutputAmountsResponse response = GetOutputAmountsHelper.GetOutputAmounts(request);
		Assert.Equal(new long[] { 729, 261 }, response.OutputAmounts);
	}
}
