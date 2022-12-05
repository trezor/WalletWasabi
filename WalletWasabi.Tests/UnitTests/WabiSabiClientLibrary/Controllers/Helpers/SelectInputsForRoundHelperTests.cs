using NBitcoin;
using System.Collections.Generic;
using System.Collections.Immutable;
using WalletWasabi.WabiSabi.Backend.Rounds;
using WalletWasabi.WabiSabi.Models;
using WalletWasabi.WabiSabiClientLibrary.Controllers.Helpers;
using WalletWasabi.WabiSabiClientLibrary.Models;
using WalletWasabi.WabiSabiClientLibrary.Models.SelectUtxoForRound;
using Xunit;

namespace WalletWasabi.Tests.UnitTests.WabiSabiClientLibrary.Controllers.Helpers;

/// <summary>
/// Tests for <see cref="SelectInputsForRoundHelper"/>
/// </summary>
public class SelectInputsForRoundHelperTests 
{
	[Fact]
	public void NoUtxoTest()
	{
		Utxo[] utxos = Array.Empty<Utxo>();

		SelectInputsForRoundRequest request = new(utxos, AnonScoreTarget: 50, Constants: MakeDefaultConstants(), SemiPrivateThreshold: 2, LiquidityClue: Money.Zero);
		SelectInputsForRoundResponse response = SelectInputsForRoundHelper.SelectInputsForRound(request);
		Assert.Empty(response.Indices);
	}

	[Fact]
	public void SingleNonPrivateUtxoTest()
	{
		Utxo[] utxos = new Utxo[]
		{
			// Not allowed script type.
			new(new OutPoint(new uint256("06d0e3ae26dc6a98da5ea16d19eb6ad2817aab3f510d91c13de2ea9457124258"), 0), Amount: Money.Coins(0.02m), ScriptType.Witness, AnonymitySet: 10, LastCoinjoinTimestamp: 1653421698),

			// Anonymity set is higher than target 50. Might and might not be in the result.
			new(new OutPoint(new uint256("f35481573468b5e4f4a4fce6afb2c3efb5e7f9b18ad5413e45ce07a1de315d7c"), 0), Amount: Money.Coins(0.02m), ScriptType.P2WPKH, AnonymitySet: 60, LastCoinjoinTimestamp: 1653421698),

			// 0.009 is not sufficient amount, minimum allowed value is 0.01.
			new(new OutPoint(new uint256("dfb38af06d063128af9c4483bf944cc38c6608749cc145be2b9912ef7e185450"), 0), Amount: Money.Coins(0.009m), ScriptType.P2WPKH, AnonymitySet: 10, LastCoinjoinTimestamp: 1653421698),

			// Ok.
			new(new OutPoint(new uint256("6a8cb2d81062ef93ae5d58b5cbe78d5fc5159f609e0d06f767d2f8eae5ead907"), 0), Amount: Money.Coins(0.015m), ScriptType.P2WPKH, AnonymitySet: 10, LastCoinjoinTimestamp: 1653421698),
		};

		SelectInputsForRoundRequest request = new(utxos, AnonScoreTarget: 50, Constants: MakeDefaultConstants(), SemiPrivateThreshold: 2, LiquidityClue: Money.Zero);
		SelectInputsForRoundResponse response = SelectInputsForRoundHelper.SelectInputsForRound(request);

		Array.Sort(response.Indices);

		// The first possible result and the second possible result.
		if (response.Indices.Length == 1)
		{
			Assert.Equal(ImmutableArray.Create(3), response.Indices);
		}
		else
		{
			Assert.Equal(ImmutableArray.Create(1, 3), response.Indices);
		}
	}

	[Fact]
	public void TwoUtxoWithHighAnonScoreTargetTest()
	{
		Utxo[] utxos = new Utxo[]
		{
			new(new OutPoint(new uint256("dfb38af06d063128af9c4483bf944cc38c6608749cc145be2b9912ef7e185450"), 0), Amount: Money.Coins(0.01m), ScriptType.P2WPKH, AnonymitySet: 90, LastCoinjoinTimestamp: 1653421698),
			new(new OutPoint(new uint256("f35481573468b5e4f4a4fce6afb2c3efb5e7f9b18ad5413e45ce07a1de315d7c"), 1), Amount: Money.Coins(0.01m), ScriptType.P2WPKH, AnonymitySet: 90, LastCoinjoinTimestamp: 1653421698),
		};

		SelectInputsForRoundRequest request = new(utxos, AnonScoreTarget: 50, Constants: MakeDefaultConstants(), SemiPrivateThreshold: 2, LiquidityClue: Money.Zero);
		SelectInputsForRoundResponse response = SelectInputsForRoundHelper.SelectInputsForRound(request);
		Assert.Empty(response.Indices);
	}

	private static UtxoSelectionParameters MakeDefaultConstants()
		=> new(
			AllowedInputAmounts: new MoneyRange(Money.Coins(0.01m), Money.Coins(0.05m)),
			AllowedOutputAmounts: new MoneyRange(Money.Coins(0.01m), Money.Coins(0.05m)),
			AllowedInputScriptTypes: (new ScriptType[] { ScriptType.P2WPKH }).ToImmutableSortedSet(),
			CoordinationFeeRate: CoordinationFeeRate.Zero,
			MiningFeeRate: new FeeRate(5m));
}
