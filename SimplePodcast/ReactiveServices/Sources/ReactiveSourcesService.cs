using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Disposables.Fluent;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using Core;
using ReactiveUI;

namespace SimplePodcast;

public sealed class ReactiveSourcesService : ReactiveServiceBase, IReactiveSourcesService
{
    private readonly ISourcesService _sourcesService;
    private readonly Subject<ISourceData> _onSourceAdded;
    private readonly Subject<ISourceData> _onSourceRemoved;
    
    public ReactiveSourcesService(ISourcesService sourcesService)
    {
        _sourcesService = sourcesService;
        _onSourceAdded = new Subject<ISourceData>().DisposeWith(Disposables);
        _onSourceRemoved = new Subject<ISourceData>().DisposeWith(Disposables);
        OnSourcesChanged = Observable.Merge(
            _onSourceAdded.Select(s => new SourceChangedInfo(s, SourcesChangeReason.Added)),
            _onSourceRemoved.Select(s => new SourceChangedInfo(s, SourcesChangeReason.Removed))
        );
        SourcesStream = Observable
            .Interval(TimeSpan.FromMinutes(2))
            .SelectMany(_ => Observable.FromAsync(async ct =>
                {
                    try
                    {
                        return await sourcesService.GetAllSourcesAsync(cancel: ct).ConfigureAwait(false);
                    }
                    catch (Exception)
                    {
                        // TODO: add logger
                    }
                        
                    return [];
                })
            ).SelectMany(sources => sources);
    }

    public IObservable<SourceChangedInfo> OnSourcesChanged { get; }
    public IObservable<ISourceData> OnSourceAdded => _onSourceAdded;
    public IObservable<ISourceData> OnSourceRemoved => _onSourceRemoved;
    public IObservable<ISourceData> SourcesStream { get; }

    public async Task AddSourceAsync(ISourceData sourceData, CancellationToken cancel = default)
    {
        await _sourcesService.AddSourceAsync(sourceData, cancel).ConfigureAwait(false);
        _onSourceAdded.OnNext(sourceData);
    }

    public async Task RemoveSourceAsync(ISourceData sourceUri, CancellationToken cancel = default)
    {
        await _sourcesService.RemoveSourceAsync(sourceUri, cancel).ConfigureAwait(false);
        _onSourceRemoved.OnNext(sourceUri);
    }

    public async Task<IEnumerable<ISourceData>> GetAllSourcesAsync(CancellationToken cancel = default)
    {
        return await _sourcesService.GetAllSourcesAsync(cancel).ConfigureAwait(false);
    }
}