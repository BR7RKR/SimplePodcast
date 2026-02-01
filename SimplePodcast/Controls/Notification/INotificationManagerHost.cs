using Ursa.Controls;

namespace SimplePodcast;

public interface INotificationManagerHost
{
    public INotificationManager GetNotificationManager();
}