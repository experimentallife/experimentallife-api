namespace Project.Common.Extention;

public static partial class ExtObject
{
  public static long ToUnixTimeStampMillisecond(this DateTime time)
  {
    DateTime startTime = new DateTime(1970, 1, 1, 0, 0, 0).ToLocalTime();
    return (long)(time - startTime).TotalMilliseconds;
  }
}