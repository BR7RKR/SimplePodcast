namespace Db;

public interface IPagination<TEntity>
{
    public Task<IEnumerable<TEntity>> GetManyAsync(
        Action<TEntity>? predicate = null,
        int? skip = null,
        int? take = null,
        CancellationToken cancel = default
    );
}