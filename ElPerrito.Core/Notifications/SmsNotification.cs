using System;
using System.Threading.Tasks;
using ElPerrito.Core.Logging;

namespace ElPerrito.Core.Notifications
{
    /// <summary>
    /// Implementación de notificación por SMS
    /// </summary>
    public class SmsNotification : INotification
    {
        private readonly Logger _logger = Logger.Instance;

        public async Task<bool> SendAsync(string recipient, string subject, string message)
        {
            try
            {
                // Simulación de envío de SMS
                await Task.Delay(50); // Simular latencia de red

                _logger.LogInfo($"SMS enviado a {recipient}");
                Console.WriteLine($"[SMS] To: {recipient}\nMessage: {message}");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al enviar SMS a {recipient}", ex);
                return false;
            }
        }

        public string GetNotificationType() => "SMS";
    }
}
