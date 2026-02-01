using System;
using Ursa.Controls;

namespace SimplePodcast;

public static class NotificationManagerMixin
{
    extension(INotificationManager notificationManager)
    {
        public void ShowSuccess(
            string title,
            string? bodyMessage = null,
            bool showClose = false,
            TimeSpan? duration = null,
            Action? onClose = null,
            Action? onOpen = null
        ) => notificationManager.Show(new Success(title, bodyMessage, showClose, duration, onClose, onOpen));

        public void ShowError(
            string title,
            string bodyMessage,
            bool showClose = true,
            TimeSpan? duration = null,
            Action? onClose = null,
            Action? onOpen = null
        ) => notificationManager.Show(new Error(title, bodyMessage, showClose, duration, onClose, onOpen));

        public void ShowError(
            string title,
            Exception exception,
            bool showClose = true,
            TimeSpan? duration = null,
            Action? onClose = null,
            Action? onOpen = null
        ) => notificationManager.Show(new Error(title, exception, showClose, duration, onClose, onOpen));

        public void ShowWarning(
            string title,
            string bodyMessage,
            bool showClose = true,
            TimeSpan? duration = null,
            Action? onClose = null,
            Action? onOpen = null
        ) => notificationManager.Show(new Warning(title, bodyMessage, showClose, duration, onClose, onOpen));
    }
}