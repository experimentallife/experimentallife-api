using Project.Common.Extention;

namespace Project.Common.Model;

public class ActionResultVm
{
  public int Status { get; set; } = StatusCodes.Status200OK;

  public string Error { get; set; }

  public string Message { get; set; }

  public string Timestamp { get; set; } = DateTime.Now.ToUnixTimeStampMillisecond().ToString();

  public string Path { get; set; }
}