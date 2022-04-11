using NBitcoin;
using System.Collections.Generic;
using System.Linq;
using WalletWasabi.WabiSabiClientLibrary.Models;
using WalletWasabi.WabiSabiClientLibrary.Models.DecomposeAmounts;
using WalletWasabi.WabiSabi.Client;
using WalletWasabi.WabiSabi.Models;

namespace WalletWasabi.WabiSabiClientLibrary.Controllers.Helpers;

public class DecomposeAmountsHelper
{
	public static DecomposeAmountsResponse Decompose(DecomposeAmountsRequest request)
	{
		Constants constants = request.Constants;
		MoneyRange allowedOutputAmounts = request.Constants.AllowedOutputAmounts;
		int inputVirtualSize = WalletWasabi.Helpers.Constants.P2wpkhInputMaximumVirtualSize;

		AmountDecomposer decomposer = new(constants.FeeRate, allowedOutputAmounts, request.OutputSize, inputVirtualSize, request.AvailableVsize);

		IEnumerable<Money> myInputCoinEffectiveValues = request.InternalAmounts.Select(x => Money.FromUnit(x, MoneyUnit.Satoshi));
		IEnumerable<Money> othersInputCoinEffectiveValues = request.ExternalAmounts.Select(x => Money.FromUnit(x, MoneyUnit.Satoshi));

		IEnumerable<Money> response = decomposer.Decompose(myInputCoinEffectiveValues, othersInputCoinEffectiveValues);
		long[] result = response.Select(x => x.Satoshi).ToArray();

		return new DecomposeAmountsResponse(result);
	}
}
