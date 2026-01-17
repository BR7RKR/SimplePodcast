using Core;

namespace SimplePodcast.ViewModels;


public abstract class SourceTypeViewModelBase : ViewModelBase
{
    public abstract string Title { get; }

    public abstract ISourceData? GetSourceData();
}