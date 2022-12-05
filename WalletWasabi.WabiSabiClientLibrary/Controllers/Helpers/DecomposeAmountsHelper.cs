using NBitcoin;
using System.Collections.Generic;
using System.Linq;
using WalletWasabi.WabiSabiClientLibrary.Models;
using WalletWasabi.WabiSabi.Client;
using WalletWasabi.WabiSabi.Models;

namespace WalletWasabi.WabiSabiClientLibrary.Controllers.Helpers;

public class DecomposeAmountsHelper
{
	public static DecomposeAmountsResponse Decompose(DecomposeAmountsRequest request)
	{
		AmountDecomposer decomposer = new(request.FeeRate, request.AllowedOutputAmounts, request.OutputSize, request.InputSize, request.AvailableVsize);

		IEnumerable<Money> myInputCoinEffectiveValues = request.InternalAmounts.Select(x => Money.FromUnit(x, MoneyUnit.Satoshi));
		IEnumerable<Money> othersInputCoinEffectiveValues = request.ExternalAmounts.Select(x => Money.FromUnit(x, MoneyUnit.Satoshi));

		IEnumerable<Money> response = decomposer.Decompose(myInputCoinEffectiveValues, othersInputCoinEffectiveValues);
		long[] result = response.Select(x => x.Satoshi).ToArray();

		return new DecomposeAmountsResponse(result);
	}
}
