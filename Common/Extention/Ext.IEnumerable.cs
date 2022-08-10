namespace Project.Common.Extention;

public static partial class ExtObject
{
  public static void ForEach<T>(this IEnumerable<T> iEnumberable, Action<T> func)
  {
    foreach (var item in iEnumberable)
      func(item);
  }
}