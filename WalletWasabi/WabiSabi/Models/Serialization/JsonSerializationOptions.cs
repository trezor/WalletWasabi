using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using WalletWasabi.JsonConverters;
using WalletWasabi.JsonConverters.Bitcoin;
using WalletWasabi.JsonConverters.Timing;
using WalletWasabi.WabiSabi.Crypto.Serialization;

namespace WalletWasabi.WabiSabi.Models.Serialization;

public class JsonSerializationOptions
{
	private static readonly JsonSerializerSettings CurrentSettings = new()
	{
		ContractResolver = new RequireObjectPropertiesContractResolver(),
		MissingMemberHandling = MissingMemberHandling.Error,
		Converters = new List<JsonConverter>()
			{
				new ScalarJsonConverter(),
				new GroupElementJsonConverter(),
				new OutPointJsonConverter(),
				new WitScriptJsonConverter(),
				new ScriptJsonConverter(),
				new OwnershipProofJsonConverter(),
				new NetworkJsonConverter(),
				new FeeRateJsonConverter(),
				new MoneySatoshiJsonConverter(),
				new Uint256JsonConverter(),
				new MultipartyTransactionStateJsonConverter(),
				new ExceptionDataJsonConverter(),
				new ExtPubKeyJsonConverter(),
				new TimeSpanJsonConverter(),
				new CoinJsonConverter(),
				new CoinJoinEventJsonConverter(),
			}
	};
	public static readonly JsonSerializationOptions Default = new();

	private JsonSerializationOptions()
	{
	}

	public JsonSerializerSettings Settings => CurrentSettings;

	// Taken from https://stackoverflow.com/a/29660550
	private class RequireObjectPropertiesContractResolver : DefaultContractResolver
	{
		protected override JsonObjectContract CreateObjectContract(Type objectType)
		{
			JsonObjectContract contract = base.CreateObjectContract(objectType);
			contract.ItemRequired = Required.AllowNull;
			return contract;
		}
	}
}
