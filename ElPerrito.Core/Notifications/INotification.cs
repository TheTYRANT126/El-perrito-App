using System.Threading.Tasks;

namespace ElPerrito.Core.Notifications
{
    /// <summary>
    /// Interfaz base para notificaciones
    /// </summary>
    public interface INotification
    {
        Task<bool> SendAsync(string recipient, string subject, string message);
        string GetNotificationType();
    }
}
