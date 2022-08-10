using Newtonsoft.Json;

namespace Project.Common.ClassLibrary;

public class JsonConverterLong : JsonConverter
{
  public override bool CanConvert(Type objectType)
  {
    return true;
  }

  public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
      JsonSerializer serializer)
  {
    if ((reader.ValueType == null || reader.ValueType == typeof(long?)) && reader.Value == null)
    {
      return null;
    }

    long.TryParse(reader.Value != null ? reader.Value.ToString() : string.Empty, out long value);
    return value;
  }

  public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
  {
    if (value == null)
      writer.WriteValue(value);
    else
      writer.WriteValue(value + string.Empty);
  }
}