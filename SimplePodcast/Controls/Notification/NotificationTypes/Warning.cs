using System;
using Avalonia.Controls.Notifications;
using Notification = Ursa.Controls.Notification;

namespace SimplePodcast;

public sealed class Warning(
    string title,
    string bodyMessage,
    bool showClose = true,
    TimeSpan? duration = null,
    Action? onClose = null,
    Action? onOpen = null
)
    : Notification(
        title,
        bodyMessage,
        NotificationType.Warning,
        duration ?? DefaultDuration,
        showClose,
        onClose,
        onOpen
    )
{
    public Warning() 
        : this("Warning", "Something is wrong")
    {
        DesignTime.ThrowIfNotDesignTime();
    }

    public static TimeSpan DefaultDuration { get; } = TimeSpan.FromMilliseconds(4000);
}