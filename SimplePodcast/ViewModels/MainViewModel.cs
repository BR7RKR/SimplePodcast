using System;
using System.ComponentModel.Design;
using System.Reactive.Disposables.Fluent;
using System.Reactive.Linq;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;

namespace SimplePodcast.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly ObservableAsPropertyHelper<bool> _isSettingsVisible;

    public MainViewModel() 
        : this(new HeaderViewModel(), new ServiceContainer())
    {
        DesignTime.ThrowIfNotDesignTime();
    }

    public MainViewModel(HeaderViewModel headerViewModel, IServiceProvider services)
    {
        HeaderViewModel = headerViewModel.DisposeWith(Disposables);
        
        _isSettingsVisible = HeaderViewModel.WhenAnyValue(vm => vm.IsSettingsVisible)
            .ToProperty(
                this, 
                vm => vm.IsSettingsVisible,
                scheduler: RxApp.MainThreadScheduler, 
                initialValue: false
            ).DisposeWith(Disposables);
        
        this.WhenAnyValue(x => x.IsSettingsVisible)
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe(isSettings =>
            {
                CurrentContent?.Dispose();
                CurrentContent = isSettings
                    ? services.GetRequiredService<SettingsViewModel>()
                    : services.GetRequiredService<PodcastsListViewModel>();
            })
            .DisposeWith(Disposables);
    }
    
    public bool IsSettingsVisible => _isSettingsVisible.Value;
    
    public HeaderViewModel HeaderViewModel { get; }

    public ViewModelBase? CurrentContent
    {
        get;
        private set => this.RaiseAndSetIfChanged(ref field, value);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            CurrentContent?.Dispose();
        }
        
        base.Dispose(disposing);
    }
} 