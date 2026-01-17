using Db.Models;

namespace Core;

public interface ISourceData
{
    public int Id { get; }
    public string Url { get; }
    public SourceType Type { get; }
}
