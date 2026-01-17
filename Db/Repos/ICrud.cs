namespace Db;

public interface ICrud<in TKey, TEntity>
{
    public Task CreateAsync(TEntity entity, CancellationToken cancel = default);
    
    public Task UpdateAsync(TKey id, TEntity entity, CancellationToken cancel = default);
    
    public Task DeleteAsync(TKey id, CancellationToken cancel = default);
    
    public Task<TEntity?> GetAsync(TKey id, CancellationToken cancel = default);
}