using NBitcoin;
using System.ComponentModel.DataAnnotations;
using WalletWasabi.Crypto;

namespace WalletWasabi.WabiSabi.Models;

public class CoinWithOwnershipProof : Coin
{
	public CoinWithOwnershipProof(Coin coin, OwnershipProof ownershipProof) : base(coin.Outpoint, coin.TxOut)
	{
		OwnershipProof = ownershipProof;
	}

	public CoinWithOwnershipProof(OutPoint outpoint, TxOut txOut, OwnershipProof ownershipProof) : base(outpoint, txOut)
	{
		OwnershipProof = ownershipProof;
	}

	public OwnershipProof OwnershipProof { get; set; }
}
