using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Project.Common.Extention;

public static partial class ExtObject
{
  public static JObject ToJObject(this string jsonStr)
  {
    return jsonStr == null ? JObject.Parse("{}") : JObject.Parse(jsonStr.Replace("&nbsp;", ""));
  }

  public static object ToObject(this string jsonStr, Type type)
  {
    return JsonConvert.DeserializeObject(jsonStr, type);
  }
}