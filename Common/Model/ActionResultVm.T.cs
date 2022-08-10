namespace Project.Common.Model;

public class ActionResultVm<T>
{
  public List<T> Content { get; set; }

  public int TotalElements { get; set; }
}