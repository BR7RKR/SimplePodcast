using Microsoft.Extensions.DependencyInjection;
using SimplePodcast.ViewModels;
using Ursa.Controls;

namespace SimplePodcast;

public static class CommonDependencies
{
    public static IServiceCollection AddCommonDependencies(this IServiceCollection services)
    {
        // ViewModels
        services.AddTransient<MainViewModel>();
        services.AddTransient<SettingsViewModel>();
        services.AddTransient<HeaderViewModel>();
        
        // Services
        services.AddSingleton<INotificationManagerHost, NotificationManagerHost>();
        
        return services;
    }
}