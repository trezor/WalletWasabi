using NBitcoin;
using Newtonsoft.Json;

namespace WalletWasabi.JsonConverters;

public class ScriptTypeJsonConverter : JsonConverter
{
	/// <inheritdoc />
	public override bool CanConvert(Type objectType)
	{
		return objectType == typeof(ScriptType);
	}

	/// <inheritdoc />
	public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
	{
		return (ScriptType)Enum.Parse(typeof(ScriptType), ((string)reader.Value).Trim());
	}

	/// <inheritdoc />
	public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
	{
		writer.WriteValue(((ScriptType)value).ToString());
	}
}
