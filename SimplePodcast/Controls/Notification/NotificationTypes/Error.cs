using System;
using Avalonia.Controls.Notifications;
using Notification = Ursa.Controls.Notification;

namespace SimplePodcast;

public sealed class Error(
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
        NotificationType.Error,
        duration ?? DefaultDuration,
        showClose,
        onClose,
        onOpen
    )
{
    public Error() 
        : this("Error", "Operation failed")
    {
        DesignTime.ThrowIfNotDesignTime();
    }

    public Error(
        string title, 
        Exception exception, 
        bool showClose = true,
        TimeSpan? duration = null,
        Action? onClose = null,
        Action? onOpen = null
    ) : this(title,  exception.Message, showClose, duration, onClose, onOpen)
    { }

    public static TimeSpan DefaultDuration { get; } = TimeSpan.FromMilliseconds(5000);
}