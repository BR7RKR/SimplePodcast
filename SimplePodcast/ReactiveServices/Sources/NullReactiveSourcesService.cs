using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Db.Models;
using DynamicData;

namespace SimplePodcast;

public sealed class NullReactiveSourcesService : IReactiveSourcesService
{
    public IEnumerable<ISourceData> DesignData { get; } =
    [
        new SourceData()
        { 
            Id = 0, 
            Url = "https://feeds.soundcloud.com/users/soundcloud:users:203144140/sounds.rss", 
            Type = SourceType.Rss,
        },
        new SourceData()
        {
            Id = 1,
            Url = "https://feeds.soundcloud.com/users/soundcloud:users:203144140/sounds.rss",
            Type = SourceType.Rss,
        },
        new SourceData()
        {
            Id = 2,
            Url = "https://www.youtube.com/feeds/videos.xml?channel_id=UC-9-kyTW8ZkZNDHQJ6FgpwQ",
            Type = SourceType.Youtube,
        },
    ];
    
    public Task AddSourceAsync(ISourceData sourceUri, CancellationToken cancel = default)
    {
        return Task.CompletedTask;
    }

    public Task RemoveSourceAsync(int id, CancellationToken cancel = default)
    {
        return Task.CompletedTask;
    }

    public Task<IEnumerable<ISourceData>> GetAllSourcesAsync(CancellationToken cancel = default)
    {
        return Task.FromResult(DesignData);
    }

    public IObservable<IChangeSet<ISourceData, int>> Connect()
    {
        var cache = new SourceCache<ISourceData, int>(source => source.Id);
        cache.AddOrUpdate(DesignData);
        return cache.Connect();
    }

    public Task RefreshAsync(CancellationToken cancel = default)
    {
        return Task.CompletedTask;
    }
}