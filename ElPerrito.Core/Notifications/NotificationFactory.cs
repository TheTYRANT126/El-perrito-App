using System;

namespace ElPerrito.Core.Notifications
{
    /// <summary>
    /// Implementación del patrón Factory Method para crear notificaciones
    /// </summary>
    public enum NotificationType
    {
        Email,
        Sms,
        Push
    }

    public class NotificationFactory
    {
        /// <summary>
        /// Factory Method para crear instancias de notificaciones
        /// </summary>
        public static INotification CreateNotification(NotificationType type)
        {
            return type switch
            {
                NotificationType.Email => new EmailNotification(),
                NotificationType.Sms => new SmsNotification(),
                NotificationType.Push => new PushNotification(),
                _ => throw new ArgumentException($"Tipo de notificación no soportado: {type}")
            };
        }

        /// <summary>
        /// Factory Method sobrecargado que acepta string
        /// </summary>
        public static INotification CreateNotification(string type)
        {
            return type.ToLower() switch
            {
                "email" or "correo" => new EmailNotification(),
                "sms" or "mensaje" => new SmsNotification(),
                "push" or "notificacion" => new PushNotification(),
                _ => throw new ArgumentException($"Tipo de notificación no reconocido: {type}")
            };
        }
    }
}
