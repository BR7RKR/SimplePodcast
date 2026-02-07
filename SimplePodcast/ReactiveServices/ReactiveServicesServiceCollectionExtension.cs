using Microsoft.Extensions.DependencyInjection;

namespace SimplePodcast;

public static class ReactiveServicesServiceCollectionExtension
{
    public static IServiceCollection AddReactiveServices(this IServiceCollection services)
    {
        services.AddSingleton<IReactiveSourcesService, ReactiveSourcesService>();
        return services;
    }
}