using NBitcoin;
using Newtonsoft.Json;
using WalletWasabi.Crypto;
using NBitcoin;
using Newtonsoft.Json;
using WalletWasabi.Backend.Models;

namespace WalletWasabi.JsonConverters.Bitcoin;

public class CoinWithOwnershipProofJsonConverter : JsonConverter<CoinWithOwnershipProof>
{
	/// <inheritdoc />
	public override CoinWithOwnershipProof? ReadJson(JsonReader reader, Type objectType, CoinWithOwnershipProof? existingValue, bool hasExistingValue, JsonSerializer serializer)
	{
		var coin = serializer.Deserialize<SerializableCoinWithProof>(reader)
			?? throw new JsonSerializationException("Coin could not be deserialized.");
		return new CoinWithOwnershipProof(coin.Outpoint, coin.TxOut, coin.OwnershipProof);
	}

	/// <inheritdoc />
	public override void WriteJson(JsonWriter writer, CoinWithOwnershipProof? coin, JsonSerializer serializer)
	{
		if (coin is null)
		{
			throw new ArgumentNullException(nameof(coin));
		}

		var newCoin = new SerializableCoinWithProof(coin.Outpoint, coin.TxOut, coin.OwnershipProof);
		serializer.Serialize(writer, newCoin);
	}

	private class SerializableCoinWithProof
	{
		[JsonConstructor]
		public SerializableCoinWithProof(OutPoint outpoint, TxOut txOut, OwnershipProof ownershipProof)
		{
			Outpoint = outpoint;
			TxOut = txOut;
      OwnershipProof = ownershipProof;
		}
		public OutPoint Outpoint { get; }
		public TxOut TxOut { get; }
		public OwnershipProof OwnershipProof { get; }
	}
}
