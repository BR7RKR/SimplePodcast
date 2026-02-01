using Ursa.Controls;

namespace SimplePodcast;

public class NullNotificationManagerHost : INotificationManagerHost
{
    public INotificationManager GetNotificationManager()
    {
        return new NullNotificationManager();
    }
}