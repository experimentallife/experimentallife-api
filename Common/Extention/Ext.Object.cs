using System.ComponentModel;

using Newtonsoft.Json;

using Project.Extensions.ClassLibrary;

namespace Project.Common.Extention;

public static partial class ExtObject
{
  public static bool IsNullOrEmpty(this object obj)
  {
    if (obj == null)
      return true;
    string objStr = obj.ToString();
    return string.IsNullOrEmpty(objStr);
  }

  public static bool IsNull(this object obj)
  {
    return obj == null;
  }

  public static string ToJson(this object obj)
  {
    var serializerSettings = new JsonSerializerSettings
    {
      ContractResolver = new CustomContractResolver(),
      DateFormatString = "yyyy-MM-dd HH:mm:ss",
      ReferenceLoopHandling = ReferenceLoopHandling.Ignore
    };

    return JsonConvert.SerializeObject(obj, Formatting.None, serializerSettings);
  }

  public static object ChangeType_ByConvert(this object obj, Type targetType)
  {
    object resObj;
    if (targetType.IsGenericType && targetType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
    {
      NullableConverter newNullableConverter = new NullableConverter(targetType);
      resObj = newNullableConverter.ConvertFrom(obj);
    }
    else
    {
      resObj = Convert.ChangeType(obj, targetType);
    }

    return resObj;
  }
}