namespace Core;

public interface ISourcesService
{
    public Task AddSourceAsync(ISourceData sourceData, CancellationToken cancel = default);
    public Task RemoveSourceAsync(int id, CancellationToken cancel = default);
    public Task<IEnumerable<ISourceData>> GetAllSourcesAsync(CancellationToken cancel = default);
}