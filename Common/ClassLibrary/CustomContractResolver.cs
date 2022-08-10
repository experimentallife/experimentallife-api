using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using Project.Common.ClassLibrary;

namespace Project.Extensions.ClassLibrary;

public class CustomContractResolver : CamelCasePropertyNamesContractResolver
{
  protected override string ResolvePropertyName(string propertyName)
  {
    return propertyName.Substring(0, 1).ToLower() + propertyName.Substring(1);
  }

  protected override JsonConverter ResolveContractConverter(Type objectType)
  {
    if (objectType == typeof(long))
    {
      return new JsonConverterLong();
    }

    return base.ResolveContractConverter(objectType);
  }
}