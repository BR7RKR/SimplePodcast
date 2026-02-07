using System.Linq;
using Core;
using Db.Models;
using Material.Icons;

namespace SimplePodcast.ViewModels;

public class SourceViewModel : ViewModelBase
{
    private readonly ISourceData _base;

    public SourceViewModel() 
        : this(new NullReactiveSourcesService().DesignData.First())
    {
        DesignTime.ThrowIfNotDesignTime();
    }
    
    public SourceViewModel(ISourceData source)
    {
        _base = source;
        Icon = _base.Type == SourceType.Rss ? MaterialIconKind.RssFeed : MaterialIconKind.Youtube;
    }
    
    public int Id => _base.Id;
    public string Url => _base.Url;
    public SourceType Type => _base.Type;
    public MaterialIconKind Icon { get; }
}