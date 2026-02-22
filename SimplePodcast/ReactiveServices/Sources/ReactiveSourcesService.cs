using System;
using System.Collections.Generic;
using System.Reactive.Disposables.Fluent;
using System.Threading;
using System.Threading.Tasks;
using Core;
using DynamicData;

namespace SimplePodcast;

public sealed class ReactiveSourcesService : ReactiveServiceBase, IReactiveSourcesService
{
    private readonly ISourcesService _sourcesService;
    private readonly SourceCache<ISourceData, int> _sourcesCache;
    private readonly SemaphoreSlim _gate;
    
    public ReactiveSourcesService(ISourcesService sourcesService)
    {
        ArgumentNullException.ThrowIfNull(sourcesService);
        
        _sourcesService = sourcesService;
        _gate = new SemaphoreSlim(1, 1).DisposeWith(Disposables);
        _sourcesCache = new SourceCache<ISourceData, int>(source =>  source.Id).DisposeWith(Disposables);
    }
    
    public IObservable<IChangeSet<ISourceData, int>> Connect() => _sourcesCache.Connect();

    public async Task AddSourceAsync(ISourceData sourceData, CancellationToken cancel = default)
    {
        await _gate.WaitAsync(cancel).ConfigureAwait(false);

        try
        {
            await _sourcesService.AddSourceAsync(sourceData, cancel).ConfigureAwait(false);
            _sourcesCache.AddOrUpdate(sourceData);
        }
        finally
        {
            _gate.Release();
        }
    }

    public async Task RemoveSourceAsync(int id, CancellationToken cancel = default)
    {
        await _gate.WaitAsync(cancel).ConfigureAwait(false);

        try
        {
            await _sourcesService.RemoveSourceAsync(id, cancel).ConfigureAwait(false);
            _sourcesCache.RemoveKey(id);
        }
        finally
        {
            _gate.Release();
        }
    }

    public async Task<IEnumerable<ISourceData>> GetAllSourcesAsync(CancellationToken cancel = default)
    {
        await _gate.WaitAsync(cancel).ConfigureAwait(false);

        try
        {
            return await _sourcesService.GetAllSourcesAsync(cancel).ConfigureAwait(false);
        }
        finally
        {
            _gate.Release();
        }
    }
    
    public async Task RefreshAsync(CancellationToken cancel = default)
    {
        await _gate.WaitAsync(cancel).ConfigureAwait(false);
        
        try
        {
            var sources = await _sourcesService.GetAllSourcesAsync(cancel).ConfigureAwait(false);
            _sourcesCache.Edit(updater =>
            {
                updater.Clear();
                updater.AddOrUpdate(sources);
            });
        }
        finally
        {
            _gate.Release();
        }
    } 
}