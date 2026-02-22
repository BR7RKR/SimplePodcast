using System;
using System.Collections.ObjectModel;
using System.Reactive.Disposables.Fluent;
using System.Reactive.Linq;
using DynamicData;
using ReactiveUI;

namespace SimplePodcast.ViewModels;

public class SettingsViewModel : ViewModelBase
{
    private readonly ReadOnlyObservableCollection<SourceViewModel> _sourcesView;

    public SettingsViewModel()
        : this(new NullReactiveSourcesService())
    {
        DesignTime.ThrowIfNotDesignTime();
    }
    
    public SettingsViewModel(IReactiveSourcesService sourcesService)
    {
        sourcesService
            .ConnectWithRefresh()
            .Transform(s => new SourceViewModel(s))
            .ObserveOn(RxSchedulers.MainThreadScheduler)
            .Bind(out _sourcesView) // TODO: fix in design
            .DisposeMany()
            .Subscribe()
            .DisposeWith(Disposables);
    }
    
    public ReadOnlyObservableCollection<SourceViewModel> SourcesView => _sourcesView;
}