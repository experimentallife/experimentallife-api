using Microsoft.AspNetCore.Mvc.Filters;

namespace Project.Common.ActionExtension.Json;

public class NoJsonParamterAttribute : Attribute, IActionFilter
{
  public void OnActionExecuting(ActionExecutingContext context)
  { }

  public void OnActionExecuted(ActionExecutedContext context)
  { }
}