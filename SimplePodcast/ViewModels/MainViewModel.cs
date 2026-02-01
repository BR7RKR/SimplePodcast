using System.Reactive.Disposables.Fluent;
using ReactiveUI;
using INotificationManager = Ursa.Controls.INotificationManager;

namespace SimplePodcast.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly ObservableAsPropertyHelper<bool> _isSettingsVisible;

    public MainViewModel() 
        : this(new HeaderViewModel())
    {
        DesignTime.ThrowIfNotDesignTime();
    }

    public MainViewModel(HeaderViewModel headerViewModel)
    {
        HeaderViewModel = headerViewModel;
        
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