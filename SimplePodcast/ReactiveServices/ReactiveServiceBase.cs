using System;
using System.Reactive.Disposables;
using System.Threading;

namespace SimplePodcast;

public abstract class ReactiveServiceBase : IReactiveService
{
    private int _isDisposed;

    public CompositeDisposable Disposables { get; } = new();

    public bool IsDisposed => _isDisposed != 0;
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (Interlocked.CompareExchange(ref _isDisposed, 1, 0) == 1)
        {
            return;
        }
        
        if (disposing)
        {
            Disposables.Dispose();
        }
    }
}