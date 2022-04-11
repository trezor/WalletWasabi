using NBitcoin;
using WalletWasabi.WabiSabiClientLibrary.Controllers.Helpers;
using WalletWasabi.WabiSabiClientLibrary.Models;
using WalletWasabi.WabiSabi.Models;
using Xunit;
using WalletWasabi.WabiSabiClientLibrary.Crypto;

namespace WalletWasabi.WabiSabiClientLibrary.Tests.IntegrationTests.Controllers.Helpers;

public class GetOutputAmountsHelperTests
{
	[Fact]
	public void SimpleGetOutputAmountsTest()
	{
		GetOutputAmountsRequest request = new(
			InternalAmounts: new decimal[] { 1000m },
			ExternalAmounts: new decimal[] { 2000m },
			OutputSize: 43,
			InputSize: 58,
			AvailableVsize: 10_000,
			MiningFeeRate: new FeeRate(100L),
			AllowedOutputAmounts: new MoneyRange(Min: 10L, Max: 10_000L)
		);

		GetOutputAmountsResponse response = GetOutputAmountsHelper.GetOutputAmounts(request, new DeterministicRandom(0));
		Assert.Equal(new long[] { 729, 263 }, response.OutputAmounts);
	}
}
