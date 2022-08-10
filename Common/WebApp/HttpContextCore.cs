using Project.Common.DI;

namespace Project.Common.WebApp;

public static class HttpContextCore
{
  public static HttpContext CurrentHttpContext => AutofacHelper.GetService<IHttpContextAccessor>().HttpContext;
}