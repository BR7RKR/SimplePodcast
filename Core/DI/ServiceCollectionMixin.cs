using Microsoft.Extensions.DependencyInjection;
using YoutubeExplodeNoPolytics;

namespace Core;

public static class ServiceCollectionMixin
{
    public static IServiceCollection AddCoreDependencies(this IServiceCollection services)
    {
        // Clients
        services.AddHttpClient();
        services.AddSingleton<YoutubeClient>(sp =>
        {
            var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
            var httpClient = httpClientFactory.CreateClient();
            return new YoutubeClient(httpClient);
        });
        
        // Services
        services.AddSingleton<ISourcesService, SourcesService>();
        
        // Validators
        services.AddSingleton<ISourceDataValidator, RssSourceDataValidator>();
        services.AddSingleton<ISourceDataValidator, YoutubeSourceDataValidator>();
        return services;
    }
}