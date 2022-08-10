namespace Project.Common.AttributeExt;

[AttributeUsage(AttributeTargets.Class)]
public sealed class AutoMappingAttribute : Attribute
{
  public AutoMappingAttribute(Type sourceType, Type targetType)
  {
    SourceType = sourceType;
    TargetType = targetType;
  }

  public Type SourceType { get; }
  public Type TargetType { get; }
}