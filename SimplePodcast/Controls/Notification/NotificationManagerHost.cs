using System;
using System.Threading;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Notifications;
using INotificationManager = Ursa.Controls.INotificationManager;
using WindowNotificationManager = Ursa.Controls.WindowNotificationManager;

namespace SimplePodcast;

public class NotificationManagerHost : INotificationManagerHost
{
    private readonly Lock _lock = new ();
    private INotificationManager? _notificationManager;

    public INotificationManager GetNotificationManager()
    {
        using (_lock.EnterScope())
        {
            return _notificationManager ??= CreateManager();
        }
    }

    private static INotificationManager CreateManager()
    {
        if (Design.IsDesignMode)
        {
            return new NullNotificationManager();
        }
        
        TopLevel? topLevel = null;
        
        if (
            Application.Current?.ApplicationLifetime
            is IClassicDesktopStyleApplicationLifetime applicationLifetime
        )
        {
            var windows = applicationLifetime.Windows;
            foreach (var window in windows)
            {
                if (!window.IsActive)
                {
                    continue;
                }

                topLevel = window;
                break;
            }

            topLevel ??=
                applicationLifetime.MainWindow
                ?? throw new NotSupportedException("No TopLevel root found");
        }
        else if (Application.Current?.ApplicationLifetime is ISingleViewApplicationLifetime sl)
        {
            topLevel =
                TopLevel.GetTopLevel(sl.MainView)
                ?? throw new NotSupportedException("No TopLevel root found");
        }
        else
        {
            throw new InvalidOperationException(
                "No TopLevel found and no ApplicationLifetime is set. "
            );
        }

        WindowNotificationManager.TryGetNotificationManager(topLevel, out var manager);
        return manager 
               ?? new WindowNotificationManager(topLevel) 
               { 
                   MaxItems = 3, 
                   Position = NotificationPosition.BottomCenter, 
               };
    }
}