using System;

namespace ElPerrito.WPF.Models
{
    public enum UserType
    {
        None,
        Cliente,
        Admin,
        Operador
    }

    public class CurrentSession
    {
        private static CurrentSession? _instance;
        private static readonly object _lock = new object();

        public static CurrentSession Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new CurrentSession();
                        }
                    }
                }
                return _instance;
            }
        }

        private CurrentSession()
        {
            IsAuthenticated = false;
            UserType = UserType.None;
        }

        public bool IsAuthenticated { get; private set; }
        public UserType UserType { get; private set; }
        public int UserId { get; private set; }
        public string UserName { get; private set; } = string.Empty;
        public string UserLastName { get; private set; } = string.Empty;
        public string UserEmail { get; private set; } = string.Empty;

        public void LoginAsCliente(int clienteId, string nombre, string apellido, string email)
        {
            IsAuthenticated = true;
            UserType = UserType.Cliente;
            UserId = clienteId;
            UserName = nombre;
            UserLastName = apellido;
            UserEmail = email;
        }

        public void LoginAsUsuario(int usuarioId, string nombre, string apellido, string email, string rol)
        {
            IsAuthenticated = true;
            UserType = rol.ToLower() == "admin" ? UserType.Admin : UserType.Operador;
            UserId = usuarioId;
            UserName = nombre;
            UserLastName = apellido ?? string.Empty;
            UserEmail = email;
        }

        public void Logout()
        {
            IsAuthenticated = false;
            UserType = UserType.None;
            UserId = 0;
            UserName = string.Empty;
            UserLastName = string.Empty;
            UserEmail = string.Empty;
        }

        public string GetFullName()
        {
            return string.IsNullOrWhiteSpace(UserLastName)
                ? UserName
                : $"{UserName} {UserLastName}";
        }

        public bool IsCliente => UserType == UserType.Cliente;
        public bool IsAdmin => UserType == UserType.Admin;
        public bool IsOperador => UserType == UserType.Operador;
        public bool IsAdminOrOperador => IsAdmin || IsOperador;
    }
}
