using System;
using System.Reactive.Disposables;
using System.Runtime.CompilerServices;
using System.Threading;
using ReactiveUI.Validation.Helpers;

namespace SimplePodcast.ViewModels;

public abstract class ViewModelBase : ReactiveValidationObject
{
    private int _isDisposed;
    protected ViewModelBase()
    {
        Disposables = new CompositeDisposable();
    }
    
    public CompositeDisposable Disposables { get; }
    public bool IsDisposed => _isDisposed != 0;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected void ThrowIfDisposed()
    {
        if (_isDisposed == 0)
        {
            return;
        }

        throw new ObjectDisposedException(GetType().FullName);
    }
    
    protected override void Dispose(bool disposing)
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