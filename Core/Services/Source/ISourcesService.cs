namespace Core;

public interface ISourcesService
{
    public Task AddSourceAsync(ISourceData sourceData, CancellationToken cancel = default);
}