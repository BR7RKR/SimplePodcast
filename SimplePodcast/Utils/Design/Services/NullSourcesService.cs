using System.Threading;
using System.Threading.Tasks;
using Core;

namespace SimplePodcast;

public class NullSourcesService : ISourcesService
{
    public Task AddSourceAsync(ISourceData sourceUri, CancellationToken cancel = default)
    {
        return Task.CompletedTask;
    }
}