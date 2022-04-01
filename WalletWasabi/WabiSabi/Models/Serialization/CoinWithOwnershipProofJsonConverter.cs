using NBitcoin;
using Newtonsoft.Json;
using WalletWasabi.Crypto;
using WalletWasabi.WabiSabi.Models;


namespace WalletWasabi.JsonConverters.Bitcoin;

public class CoinWithOwnershipProofJsonConverter : JsonConverter<CoinWithOwnershipProof>
{
	/// <inheritdoc />
	public override CoinWithOwnershipProof? ReadJson(JsonReader reader, Type objectType, CoinWithOwnershipProof? existingValue, bool hasExistingValue, JsonSerializer serializer)
	{
		var coinWithOwnershipProof = serializer.Deserialize<SerializableCoinWithOwnershipProof>(reader)
			?? throw new JsonSerializationException("CoinWithOwnershipProof could not be deserialized.");
		return new CoinWithOwnershipProof(coinWithOwnershipProof.Outpoint, coinWithOwnershipProof.TxOut, coinWithOwnershipProof.OwnershipProof);
	}

	/// <inheritdoc />
	public override void WriteJson(JsonWriter writer, CoinWithOwnershipProof? coinWithOwnershipProof, JsonSerializer serializer)
	{
		if (coinWithOwnershipProof is null)
		{
			throw new ArgumentNullException(nameof(coinWithOwnershipProof));
		}

		var newCoinWithOwnershipProof = new SerializableCoinWithOwnershipProof(coinWithOwnershipProof.Outpoint, coinWithOwnershipProof.TxOut, coinWithOwnershipProof.OwnershipProof);
		serializer.Serialize(writer, newCoinWithOwnershipProof);
	}

	private class SerializableCoinWithOwnershipProof
	{
		[JsonConstructor]
		public SerializableCoinWithOwnershipProof(OutPoint outpoint, TxOut txOut, OwnershipProof ownershipProof)
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
