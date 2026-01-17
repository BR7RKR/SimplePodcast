using Db.Models;
using YoutubeExplodeNoPolytics;
using YoutubeExplodeNoPolytics.Common;

namespace Core;

public sealed class YoutubeSourceDataValidator : ISourceDataValidator
{
    private readonly YoutubeClient _youtubeClient;
    
    public YoutubeSourceDataValidator(YoutubeClient youtubeClient)
    {
        _youtubeClient = youtubeClient;
    }

    public SourceType SupportedType => SourceType.Youtube;
    
    public async ValueTask<bool> IsValidAsync(ISourceData sourceData, CancellationToken cancel = default)
    {
        var fromYoutube = await _youtubeClient.Search.GetVideosAsync(sourceData.Url, cancel);

        return fromYoutube.Count != 0;
    }
}