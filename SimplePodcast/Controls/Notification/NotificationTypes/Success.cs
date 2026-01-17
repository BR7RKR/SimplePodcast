using System;
using Avalonia.Controls.Notifications;
using Notification = Ursa.Controls.Notification;

namespace SimplePodcast;

public sealed class Success(
    string title,
    string? bodyMessage = null,
    bool showClose = false,
    TimeSpan? duration = null,
    Action? onClose = null,
    Action? onOpen = null)
    : Notification(title,
        bodyMessage,
        NotificationType.Success,
        duration ?? DefaultDuration,
        showClose,
        onClose,
        onOpen)
{
    public Success() 
        : this("Success", "Operation completed successfully")
    {
        DesignTime.ThrowIfNotDesignTime();
    }

    public static TimeSpan DefaultDuration { get; } = TimeSpan.FromMilliseconds(3000);
}