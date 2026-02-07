using System;
using Core;

namespace SimplePodcast;

public enum SourcesChangeReason
{
    Added,
    Removed,
    Updated
}

public record SourceChangedInfo(ISourceData Source, SourcesChangeReason Reason);

public interface IReactiveSourcesService : ISourcesService
{
    public IObservable<SourceChangedInfo> OnSourcesChanged { get; }
    public IObservable<ISourceData> OnSourceAdded { get; }
    public IObservable<ISourceData> OnSourceRemoved { get; }
    public IObservable<ISourceData> SourcesStream { get; }
}