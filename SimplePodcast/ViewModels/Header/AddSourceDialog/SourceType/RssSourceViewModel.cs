using ReactiveUI.SourceGenerators;

namespace SimplePodcast.ViewModels;

public sealed class RssSourceViewModel : SourceTypeViewModelBase
{
    [Reactive]
    public string? Rss { get; set; }

    public override string Title => "Rss";
}