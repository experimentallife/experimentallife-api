using System.Reflection;

using AutoMapper;

using Project.Common.AttributeExt;
using Project.Common.Global;

namespace Project.Extensions.AutoMapper;

public class CustomProfile : Profile
{
  public CustomProfile()
  {
    InitAutoMapper();
  }

  private void InitAutoMapper()
  {
    List<(Type sourceType, Type targetType)> maps = new List<(Type sourceType, Type targetType)>();
    var atributes = GlobalData.FxAllTypes
        .Where(x => x.GetCustomAttribute<AutoMappingAttribute>() != null)
        .Select(x => x.GetCustomAttribute<AutoMappingAttribute>());

    foreach (var atribute in atributes)
    {
      if (atribute != null)
      {
        maps.Add((atribute.SourceType, atribute.TargetType));
        maps.Add((atribute.TargetType, atribute.SourceType));
      }
    }

    maps.ForEach(aMap => { CreateMap(aMap.sourceType, aMap.targetType); });
  }
}