using System;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core;
using DynamicData;

namespace SimplePodcast;

public interface IReactiveSourcesService : ISourcesService
{
    public IObservable<IChangeSet<ISourceData, int>> ConnectWithRefresh()
    {
        return Observable.Defer(() =>
            Observable.FromAsync(RefreshAsync)
                .SelectMany(_ => Connect()));
    }
    
    public IObservable<IChangeSet<ISourceData, int>> Connect();
    public Task RefreshAsync(CancellationToken cancel = default);
}