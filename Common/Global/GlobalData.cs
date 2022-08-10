using System.Reflection;

namespace Project.Common.Global;

public static class GlobalData
{
  public static readonly List<Type> FxAllTypes = Assembly.GetExecutingAssembly().GetTypes().ToList();
  public static readonly List<Type> EntityTypes = Assembly.GetExecutingAssembly().GetTypes().ToList();
}