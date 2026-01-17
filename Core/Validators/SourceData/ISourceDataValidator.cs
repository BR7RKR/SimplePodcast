using Db.Models;

namespace Core;

public interface ISourceDataValidator
{
    SourceType SupportedType { get; }
    ValueTask<bool> IsValidAsync(ISourceData sourceData, CancellationToken cancel = default);
}