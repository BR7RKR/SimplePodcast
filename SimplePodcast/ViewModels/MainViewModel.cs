using System;
using System.Reactive.Disposables.Fluent;
using System.Reactive.Linq;
using ReactiveUI;

namespace SimplePodcast.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly ObservableAsPropertyHelper<bool> _isSettingsVisible;

    public MainViewModel()
    { 
        HeaderViewModel = new HeaderViewModel().DisposeWith(Disposables); 
        
        _isSettingsVisible = HeaderViewModel.WhenAnyValue(vm => vm.IsSettingsVisible)
            .ToProperty(
                this, 
                vm => vm.IsSettingsVisible,
                scheduler: RxApp.MainThreadScheduler, 
                initialValue: false
            ).DisposeWith(Disposables);
    }
    
    public bool IsSettingsVisible => _isSettingsVisible.Value;
    
    public HeaderViewModel HeaderViewModel { get; }
} 