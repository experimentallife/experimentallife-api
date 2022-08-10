using System.Text;

using Microsoft.AspNetCore.Mvc;

using Project.Common.ActionExtension.Json;
using Project.Common.Extention;
using Project.Common.Model;

namespace Project.Controllers.Base;

[JsonParamter]
public class BaseController : ControllerBase
{
  private ContentResult JsonContent(string jsonStr)
  {
    return base.Content(jsonStr, "application/json", Encoding.UTF8);
  }

  protected ContentResult Success(string msg = "The request was successful!")
  {
    var res = new ActionResultVm
    {
      Status = StatusCodes.Status200OK,
      Message = msg,
      Path = Request.Path.Value?.ToLower()
    };

    return JsonContent(res.ToJson());
  }
}