using Project.Common.Extention;
using Project.Extensions.AutoMapper;

namespace Project.Extensions;

public static class AutoMapperSetup
{
  public static void AddAutoMapperSetup(this IServiceCollection services)
  {
    if (services.IsNull()) throw new ArgumentNullException(nameof(services));

    services.AddAutoMapper(typeof(AutoMapperConfig));
    AutoMapperConfig.RegisterMappings();
  }
}