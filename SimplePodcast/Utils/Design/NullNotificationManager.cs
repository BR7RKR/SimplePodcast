using Ursa.Controls;

namespace SimplePodcast;

public sealed class NullNotificationManager : INotificationManager
{
    public void Show(INotification notification)
    {
        return;
    }
}