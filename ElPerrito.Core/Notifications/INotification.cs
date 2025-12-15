using System.Threading.Tasks;

namespace ElPerrito.Core.Notifications
{
    public interface INotification
    {
        Task<bool> SendAsync(string recipient, string subject, string message);
        string GetNotificationType();
    }
}
