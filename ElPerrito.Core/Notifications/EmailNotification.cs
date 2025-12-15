using System;
using System.Threading.Tasks;
using ElPerrito.Core.Logging;

namespace ElPerrito.Core.Notifications
{
    public class EmailNotification : INotification
    {
        private readonly Logger _logger = Logger.Instance;

        public async Task<bool> SendAsync(string recipient, string subject, string message)
        {
            try
            {
                // Simulación de envío de email
                await Task.Delay(100); // Simular latencia de red

                _logger.LogInfo($"Email enviado a {recipient} - Asunto: {subject}");
                Console.WriteLine($"[EMAIL] To: {recipient}\nSubject: {subject}\nMessage: {message}");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al enviar email a {recipient}", ex);
                return false;
            }
        }

        public string GetNotificationType() => "Email";
    }
}
