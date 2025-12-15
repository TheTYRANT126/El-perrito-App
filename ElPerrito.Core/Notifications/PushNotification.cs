using System;
using System.Threading.Tasks;
using ElPerrito.Core.Logging;

namespace ElPerrito.Core.Notifications
{
    public class PushNotification : INotification
    {
        private readonly Logger _logger = Logger.Instance;

        public async Task<bool> SendAsync(string recipient, string subject, string message)
        {
            try
            {
                // Simulación de envío de push notification
                await Task.Delay(30); // Simular latencia de red

                _logger.LogInfo($"Push notification enviada a {recipient}");
                Console.WriteLine($"[PUSH] To: {recipient}\nTitle: {subject}\nMessage: {message}");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al enviar push notification a {recipient}", ex);
                return false;
            }
        }

        public string GetNotificationType() => "Push";
    }
}
