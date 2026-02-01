namespace Db.Models;

public class Podcast
{
    public static string TableName => "Podcasts";
    
    public int Id { get; init; }
    public string? Title { get; init; }
    public string? Description { get; init; }
    
    public required int SourceId { get; init; }
    public Source? Source { get; init; }
}