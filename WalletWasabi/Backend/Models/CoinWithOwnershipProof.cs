using NBitcoin;
using System.ComponentModel.DataAnnotations;
using WalletWasabi.Crypto;

namespace WalletWasabi.Backend.Models;

public class CoinWithOwnershipProof : Coin
{
	public OwnershipProof OwnershipProof { get; set; }

	public CoinWithOwnershipProof(Coin coin, OwnershipProof ownershipProof) : base(coin.Outpoint, coin.TxOut)
	{
		OwnershipProof = ownershipProof;
	}

	public CoinWithOwnershipProof(OutPoint outpoint, TxOut txOut, OwnershipProof ownershipProof)
	{
    Outpoint = outpoint;
    TxOut = txOut;
		OwnershipProof = ownershipProof;
	}
}
