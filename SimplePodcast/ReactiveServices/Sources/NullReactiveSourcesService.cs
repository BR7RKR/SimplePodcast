using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Db.Models;

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
    
    public IObservable<SourceChangedInfo> OnSourcesChanged => Observable.Empty<SourceChangedInfo>();
    public IObservable<ISourceData> OnSourceAdded => Observable.Empty<ISourceData>();
    public IObservable<ISourceData> OnSourceRemoved => Observable.Empty<ISourceData>();
    public IObservable<ISourceData> SourcesStream => Observable.Empty<ISourceData>();
    
    public Task AddSourceAsync(ISourceData sourceUri, CancellationToken cancel = default)
    {
        return Task.CompletedTask;
    }

    public Task RemoveSourceAsync(ISourceData sourceUri, CancellationToken cancel = default)
    {
        return Task.CompletedTask;
    }

    public Task<IEnumerable<ISourceData>> GetAllSourcesAsync(CancellationToken cancel = default)
    {
        return Task.FromResult(DesignData);
    }
}