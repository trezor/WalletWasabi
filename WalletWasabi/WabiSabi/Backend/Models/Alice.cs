using NBitcoin;
using WalletWasabi.Crypto;
using WalletWasabi.WabiSabi.Backend.Rounds;
using WalletWasabi.WabiSabi.Models;

namespace WalletWasabi.WabiSabi.Backend.Models;

public class Alice
{
	public Alice(CoinWithOwnershipProof coinWithOwnershipProof, Round round, Guid id, bool isPayingZeroCoordinationFee)
	{
		// TODO init syntax?
		Round = round;
		CoinWithOwnershipProof = coinWithOwnershipProof;
		Id = id;
		IsPayingZeroCoordinationFee = isPayingZeroCoordinationFee;
	}

	public Round Round { get; }
	public Guid Id { get; }
	public DateTimeOffset Deadline { get; set; } = DateTimeOffset.UtcNow;
	public CoinWithOwnershipProof CoinWithOwnershipProof { get; }
	public OwnershipProof OwnershipProof { get; }
	public Money TotalInputAmount => CoinWithOwnershipProof.Amount;
	public int TotalInputVsize => CoinWithOwnershipProof.ScriptPubKey.EstimateInputVsize();

	public bool ConfirmedConnection { get; set; } = false;
	public bool ReadyToSign { get; set; }
	public bool IsPayingZeroCoordinationFee { get; } = false;

	public long CalculateRemainingVsizeCredentials(int maxRegistrableSize) => maxRegistrableSize - TotalInputVsize;

	public Money CalculateRemainingAmountCredentials(FeeRate feeRate, CoordinationFeeRate coordinationFeeRate) =>
		CoinWithOwnershipProof.EffectiveValue(feeRate, IsPayingZeroCoordinationFee ? CoordinationFeeRate.Zero : coordinationFeeRate);

	public void SetDeadlineRelativeTo(TimeSpan connectionConfirmationTimeout)
	{
		// Have alice timeouts a bit sooner than the timeout of connection confirmation phase.
		Deadline = DateTimeOffset.UtcNow + (connectionConfirmationTimeout * 0.9);
	}
}
