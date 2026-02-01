using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Notifications;
using Avalonia.Markup.Xaml;
using Core;
using Db;
using Microsoft.Extensions.DependencyInjection;
using SimplePodcast.ViewModels;
using SimplePodcast.Views;

namespace SimplePodcast;

public partial class App : Application
{
    private IServiceProvider _services = null!;
    
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var services = new ServiceCollection();

        services.AddCommonDependencies();
        services.AddCoreDependencies();
        services.AddDbDependencies();
        
        _services = services.BuildServiceProvider();
        
        var db = _services.GetRequiredService<IDbContext>();
        db.Start();
        
        var vm = _services.GetRequiredService<MainViewModel>();
        
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = vm
            };
            
            desktop.ShutdownRequested += (_, _) =>
            {
                (_services as IDisposable)?.Dispose();
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = vm
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}