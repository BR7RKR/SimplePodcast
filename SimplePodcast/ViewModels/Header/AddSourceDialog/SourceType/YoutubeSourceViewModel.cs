using ReactiveUI.SourceGenerators;

namespace SimplePodcast.ViewModels;

public sealed class YoutubeSourceViewModel : SourceTypeViewModelBase
{
    [Reactive] 
    public string? Url { get; set; }

    public override string Title => "Youtube";
}