using Microsoft.AspNetCore.Mvc.Filters;

namespace Project.Common.Extention;

public static partial class ExtObject
{
  public static bool ContainsFilter<T>(this ActionExecutingContext actionExecutingContext)
  {
    return actionExecutingContext.Filters.Any(x => x.GetType() == typeof(T));
  }
}