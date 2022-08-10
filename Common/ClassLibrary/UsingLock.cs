namespace Project.Extensions.ClassLibrary;

public class UsingLock<T> : IDisposable
{
  private struct Lock : IDisposable
  {
    private ReaderWriterLockSlim _Lock;

    private bool _IsWrite;

    public Lock(ReaderWriterLockSlim rwl, bool isWrite)
    {
      _Lock = rwl;
      _IsWrite = isWrite;
    }

    public void Dispose()
    {
      if (_IsWrite)
      {
        if (_Lock.IsWriteLockHeld)
        {
          _Lock.ExitWriteLock();
        }
      }
      else
      {
        if (_Lock.IsReadLockHeld)
        {
          _Lock.ExitReadLock();
        }
      }
    }
  }

  private class Disposable : IDisposable
  {
    public static readonly Disposable Empty = new Disposable();

    public void Dispose() { }
  }

  private ReaderWriterLockSlim _LockSlim = new ReaderWriterLockSlim();

  private T _Data;

  public UsingLock()
  {
    Enabled = true;
  }

  public UsingLock(T data)
  {
    Enabled = true;
    _Data = data;
  }

  public T Data
  {
    get
    {
      if (_LockSlim.IsReadLockHeld || _LockSlim.IsWriteLockHeld)
      {
        return _Data;
      }

      throw new MemberAccessException("Please enter read or write lock mode before proceeding.");
    }
    set
    {
      if (!_LockSlim.IsWriteLockHeld)
      {
        throw new MemberAccessException("The value of Data can only be changed in write mode.");
      }

      _Data = value;
    }
  }

  public bool Enabled { get; set; }

  public IDisposable Read()
  {
    if (Enabled == false || _LockSlim.IsReadLockHeld || _LockSlim.IsWriteLockHeld)
    {
      return Disposable.Empty;
    }

    _LockSlim.EnterReadLock();
    return new Lock(_LockSlim, false);
  }

  public IDisposable Write()
  {
    if (Enabled == false || _LockSlim.IsWriteLockHeld)
    {
      return Disposable.Empty;
    }

    if (_LockSlim.IsReadLockHeld)
    {
      throw new NotImplementedException("Cannot enter write lock state in read mode.");
    }

    _LockSlim.EnterWriteLock();
    return new Lock(_LockSlim, true);
  }

  public void Dispose()
  {
    _LockSlim.Dispose();
  }
}