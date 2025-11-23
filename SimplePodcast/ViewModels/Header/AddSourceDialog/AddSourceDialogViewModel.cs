using System.Collections.Generic;
using System.Reactive.Disposables.Fluent;
using ReactiveUI;

namespace SimplePodcast.ViewModels;

public sealed class AddSourceDialogViewModel : ViewModelBase
{
    public AddSourceDialogViewModel()
    {
        var rssSource = new RssSourceViewModel().DisposeWith(Disposables);
        var youtubeSource = new YoutubeSourceViewModel().DisposeWith(Disposables);
        Tabs = [rssSource, youtubeSource];
        SelectedTab = rssSource;
    }
    
    public IReadOnlyList<ViewModelBase> Tabs { get; }

    public SourceTypeViewModelBase? SelectedTab
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }
}