using System.Diagnostics;
using System.Reactive.Disposables.Fluent;
using Core;
using Db.Models;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using ReactiveUI.Validation.Extensions;

namespace SimplePodcast.ViewModels;

public sealed class YoutubeSourceViewModel : SourceTypeViewModelBase
{
    public YoutubeSourceViewModel()
    {
        this.ValidationRule(
            vm => vm.Url, 
            rss => SourceHelper.IsUrl(rss, true), 
            "Input is not an Url. Youtube URL must be secure"
        ).DisposeWith(Disposables);
    }

    public string? Url
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public override string Title => "Youtube";
    
    public override ISourceData? GetSourceData()
    {
        if (!SourceHelper.IsUrl(Url))
        {
            return null;
        }

        Debug.Assert(Url != null, nameof(Url) + " != null");
        return new SourceData()
        {
            Url = Url,
            Type = SourceType.Youtube,
        };
    }
}