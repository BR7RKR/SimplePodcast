using System;
using System.Collections.ObjectModel;
using System.Reactive.Disposables.Fluent;
using System.Reactive.Linq;
using Core;
using Db.Models;
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
        var sourcesFromDb = new SourceCache<ISourceData, int>(source =>  source.Id)
            .DisposeWith(Disposables);
        sourcesFromDb
            .Connect()
            .Transform(s => new SourceViewModel(s))
            .ObserveOn(RxSchedulers.MainThreadScheduler)
            .Bind(out _sourcesView)
            .DisposeMany()
            .Subscribe()
            .DisposeWith(Disposables);

        sourcesService.OnSourcesChanged
            .ObserveOn(RxSchedulers.MainThreadScheduler)
            .Subscribe(info =>
            {
                switch (info.Reason)
                {
                    case SourcesChangeReason.Added:
                    case SourcesChangeReason.Updated:
                        sourcesFromDb.AddOrUpdate(info.Source);
                        break;
                    case SourcesChangeReason.Removed:
                        sourcesFromDb.RemoveKey(info.Source.Id);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }).DisposeWith(Disposables);
        
        sourcesService.SourcesStream
            .ObserveOn(RxSchedulers.MainThreadScheduler)
            .Subscribe(source => sourcesFromDb.AddOrUpdate(source))
            .DisposeWith(Disposables);
    }
    
    
    public ReadOnlyObservableCollection<SourceViewModel> SourcesView => _sourcesView;
}