using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using Project.Common.Extention;
using Project.Common.Model;
using Project.Common.WebApp;

namespace Project.Common.ActionExtension;

public class BaseActionFilter : Attribute, IAsyncActionFilter
{
  public async virtual Task OnActionExecuting(ActionExecutingContext context)
  {
    await Task.CompletedTask;
  }

  public async virtual Task OnActionExecuted(ActionExecutedContext context)
  {
    await Task.CompletedTask;
  }

  public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
  {
    await OnActionExecuting(context);
    if (context.Result == null)
    {
      var nextContext = await next();
      await OnActionExecuted(nextContext);
    }
  }

  public ContentResult JsonContent(string json)
  {
    return new ContentResult
    { Content = json, StatusCode = StatusCodes.Status200OK, ContentType = "application/json; charset=utf-8" };
  }

  public ContentResult Success()
  {
    ActionResultVm res = new ActionResultVm
    {
      Message = "The request was successful!",
      Path = HttpContextCore.CurrentHttpContext.Request.Path.Value?.ToLower()
    };

    return JsonContent(res.ToJson());
  }

  public ContentResult Success(string msg)
  {
    ActionResultVm res = new ActionResultVm
    {
      Message = msg,
      Path = HttpContextCore.CurrentHttpContext.Request.Path.Value?.ToLower()
    };

    return JsonContent(res.ToJson());
  }

  public ContentResult Success<T>(List<T> data)
  {
    ActionResultVm<T> res = new ActionResultVm<T>
    {
      Content = data,
      TotalElements = 0
    };

    return JsonContent(res.ToJson());
  }

  public ContentResult Error()
  {
    ActionResultVm res = new ActionResultVm
    {
      Status = StatusCodes.Status400BadRequest,
      Error = "BadRequest",
      Message = "Request failed!",
      Path = HttpContextCore.CurrentHttpContext.Request.Path.Value?.ToLower()
    };

    return JsonContent(res.ToJson());
  }

  public ContentResult Error(string msg)
  {
    ActionResultVm res = new ActionResultVm
    {
      Status = StatusCodes.Status400BadRequest,
      Error = "BadRequest",
      Message = msg,
      Path = HttpContextCore.CurrentHttpContext.Request.Path.Value?.ToLower()
    };

    return JsonContent(res.ToJson());
  }

  public ContentResult Error(string msg, int errorCode)
  {
    ActionResultVm res = new ActionResultVm
    {
      Status = errorCode,
      Error = "BadRequest",
      Message = msg,
      Path = HttpContextCore.CurrentHttpContext.Request.Path.Value?.ToLower()
    };

    return JsonContent(res.ToJson());
  }
}