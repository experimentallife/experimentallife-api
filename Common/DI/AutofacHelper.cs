using Autofac;

namespace Project.Common.DI;

public static class AutofacHelper
{
  public static ILifetimeScope Container { get; set; }

  public static T GetService<T>() where T : class
  {
    return Container.Resolve<T>();
  }

  public static T GetScopeService<T>() where T : class
  {
    return (T)GetService<IHttpContextAccessor>().HttpContext?.RequestServices.GetService(typeof(T));
  }
}