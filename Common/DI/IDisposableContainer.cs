namespace Project.Common.DI;

public interface IDisposableContainer : IDisposable
{
  void AddDisposableObj(IDisposable disposableObj);
}