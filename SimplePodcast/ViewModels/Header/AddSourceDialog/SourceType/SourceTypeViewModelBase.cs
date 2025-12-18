using ReactiveUI.SourceGenerators;

namespace SimplePodcast.ViewModels;

public abstract class SourceTypeViewModelBase : ViewModelBase
{
    public abstract string Title { get; }
}