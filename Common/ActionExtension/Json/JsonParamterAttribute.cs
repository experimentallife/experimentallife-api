using Microsoft.AspNetCore.Mvc.Filters;

using Project.Common.Extention;
using Project.Common.Helper;

namespace Project.Common.ActionExtension.Json;

public class JsonParamterAttribute : BaseActionFilter
{
  public override async Task OnActionExecuting(ActionExecutingContext filterContext)
  {
    if (filterContext.ContainsFilter<NoJsonParamterAttribute>())
      return;

    string contentType = filterContext.HttpContext.Request.ContentType;
    if (!contentType.IsNullOrEmpty() && contentType.Contains("application/json"))
    {
      var actionParameters = filterContext.ActionDescriptor.Parameters;
      var allParamters = HttpHelper.GetAllRequestParams(filterContext.HttpContext);
      var actionArguments = filterContext.ActionArguments;
      actionParameters.ForEach(aParamter =>
      {
        string key = aParamter.Name;
        if (allParamters.ContainsKey(key))
        {
          actionArguments[key] =
                    allParamters[key]?.ToString()?.ChangeType_ByConvert(aParamter.ParameterType);
        }
        else
        {
          try
          {
            actionArguments[key] = allParamters.ToJson().ToObject(aParamter.ParameterType);
          }
          catch
          { }
        }
      });
    }

    await Task.CompletedTask;
  }
}