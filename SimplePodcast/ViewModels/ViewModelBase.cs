using System;
using System.Reactive.Disposables;
using System.Runtime.CompilerServices;
using System.Threading;
using ReactiveUI;

namespace SimplePodcast.ViewModels;

public abstract class ViewModelBase : ReactiveObject, IDisposable
{
    private int _isDisposed;
    protected ViewModelBase()
    {
        Disposables = new CompositeDisposable();
    }
    
    public CompositeDisposable Disposables { get; }
    public bool IsDisposed => _isDisposed != 0;
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected void ThrowIfDisposed()
    {
        if (_isDisposed == 0)
        {
            return;
        }

        throw new ObjectDisposedException(GetType().FullName);
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