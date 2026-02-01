using System.ServiceModel.Syndication;
using System.Xml;
using Db.Models;

namespace Core;

public sealed class RssSourceDataValidator : ISourceDataValidator
{
    private readonly HttpClient _httpClient;
    
    public RssSourceDataValidator(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public SourceType SupportedType => SourceType.Rss;
    
    public async ValueTask<bool> IsValidAsync(ISourceData sourceData, CancellationToken cancel = default)
    {
        try
        {
            using var response = await _httpClient.GetAsync(sourceData.Url, HttpCompletionOption.ResponseHeadersRead, cancel);
            if (!response.IsSuccessStatusCode) return false;

            await using var stream = await response.Content.ReadAsStreamAsync(cancel);
            using var reader = XmlReader.Create(stream);
        
            SyndicationFeed.Load(reader);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
}