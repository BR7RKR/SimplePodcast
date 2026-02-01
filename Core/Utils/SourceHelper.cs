namespace Core;

public static class SourceHelper
{
    public static bool IsUrl(string? url, bool isHttpsOnly = false)
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            return false;
        }
        
        if (!Uri.TryCreate(url, UriKind.Absolute, out var uriResult) )
            return false;

        if (isHttpsOnly)
        {
            return uriResult.Scheme == Uri.UriSchemeHttps;
        }

        return uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps;
    }
}