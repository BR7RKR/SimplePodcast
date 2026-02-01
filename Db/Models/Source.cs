namespace Db.Models;

public enum SourceType
{
    Rss,
    Youtube
}

public class Source
{
    public static string TableName => "Sources";
    
    public int Id { get; init; }
    public required string Url { get; init; }
    public required SourceType Type { get; init; }
}