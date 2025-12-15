using System;

namespace ElPerrito.Core.Notifications
{
    public enum NotificationType
    {
        Email,
        Sms,
        Push
    }

    public class NotificationFactory
    {
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
