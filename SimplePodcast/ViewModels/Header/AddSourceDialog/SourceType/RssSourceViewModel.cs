using System.Diagnostics;
using System.Reactive.Disposables.Fluent;
using Core;
using Db.Models;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using ReactiveUI.Validation.Extensions;

namespace SimplePodcast.ViewModels;

public sealed class RssSourceViewModel : SourceTypeViewModelBase
{
    public RssSourceViewModel()
    {
        this.ValidationRule(
            vm => vm.Rss, 
            rss => SourceHelper.IsUrl(rss), 
            "Input is not an Url"
        ).DisposeWith(Disposables);
    }
    
    public string? Rss
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public override string Title => "Rss";
    
    public override ISourceData? GetSourceData()
    {
        if (!SourceHelper.IsUrl(Rss))
        {
            return null;
        }
        
        Debug.Assert(Rss != null, nameof(Rss) + " != null");
        return new SourceData
        {
            Url = Rss,
            Type = SourceType.Rss,
        };
    }
}