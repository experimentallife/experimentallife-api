using System.Net;
using System.Text;

using Project.Common.Extention;

namespace Project.Common.Helper;

public static class HttpHelper
{
  static HttpHelper()
  {
    ServicePointManager.SecurityProtocol =
        SecurityProtocolType.Tls12
        | SecurityProtocolType.Tls11
        | SecurityProtocolType.Tls;

    ServicePointManager.DefaultConnectionLimit = int.MaxValue;
    ServicePointManager.ServerCertificateValidationCallback =
        (sender, certificate, chain, sslPolicyErrors) => true;
  }

  public static Dictionary<string, object> GetAllRequestParams(HttpContext context)
  {
    Dictionary<string, object> allParams = new Dictionary<string, object>();

    var request = context.Request;
    List<string> paramKeys = new List<string>();
    var getParams = request.Query.Keys.ToList();
    var postParams = new List<string>();
    try
    {
      if (request.Method.ToLower() != "get")
        postParams = request.Form.Keys.ToList();
    }
    catch
    { }

    paramKeys.AddRange(getParams);
    paramKeys.AddRange(postParams);

    paramKeys.ForEach(aParam =>
    {
      object value = null;
      if (request.Query.ContainsKey(aParam))
      {
        value = request.Query[aParam].ToString();
      }
      else if (request.Form.ContainsKey(aParam))
      {
        value = request.Form[aParam].ToString();
      }

      if (aParam == "Password")
      {
        allParams.Add("Password", "********");
      }
      else
      {
        allParams.Add(aParam, value);
      }
    });

    string contentType = request.ContentType?.ToLower() ?? "";

    if (contentType.Contains("application/json"))
    {
      var stream = request.Body;
      string str = stream.ReadToString(Encoding.UTF8);
      if (request.Method.ToLower() == "delete")
      {
        if (!str.IsNullOrEmpty())
        {
          allParams.Add("ids", str);
        }
      }
      else
      {
        if (!str.IsNullOrEmpty())
        {
          var obj = str.ToJObject();
          foreach (var aProperty in obj)
          {
            allParams[aProperty.Key] = aProperty.Value;
          }
        }
      }
    }

    return allParams;
  }
}

public enum HttpMethod
{
  Get,
  Post,
  Put,
  Delete,
  Head,
  Options,
  Trace,
  Connect
}

public enum ContentType
{
  Form,
  Json
}
