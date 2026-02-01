namespace Db;

public interface IPagination<out TEntity>
{
    public IAsyncEnumerable<TEntity> GetManyAsync(
        Action<TEntity> predicate, 
        int skip, 
        int take, 
        CancellationToken cancel = default
    );
}