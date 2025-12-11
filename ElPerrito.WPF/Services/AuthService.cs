using System;
using System.Linq;
using System.Threading.Tasks;
using ElPerrito.Data.Context;
using ElPerrito.WPF.Models;
using Microsoft.EntityFrameworkCore;

namespace ElPerrito.WPF.Services
{
    public class AuthService
    {
        public async Task<(bool Success, string Message, string UserType)> LoginAsync(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                return (false, "Datos inválidos", string.Empty);
            }

            try
            {
                using var context = new ElPerritoContext();

                // Primero intenta con Cliente
                var cliente = await context.Clientes
                    .Where(c => c.Email == email)
                    .FirstOrDefaultAsync();

                if (cliente != null)
                {
                    if (BCrypt.Net.BCrypt.Verify(password, cliente.PasswordHash))
                    {
                        // Verificar que el cliente esté activo
                        if (cliente.Estado != "activo")
                        {
                            return (false, "Cuenta inactiva. Contacte al administrador.", string.Empty);
                        }

                        // Establecer sesión de cliente
                        CurrentSession.Instance.LoginAsCliente(
                            cliente.IdCliente,
                            cliente.Nombre,
                            cliente.Apellido,
                            cliente.Email
                        );

                        return (true, "Login exitoso", "CLIENTE");
                    }
                }

                // Si no es cliente, intenta con Usuario (Admin/Operador)
                var usuario = await context.Usuarios
                    .Where(u => u.Email == email)
                    .FirstOrDefaultAsync();

                if (usuario != null)
                {
                    if (BCrypt.Net.BCrypt.Verify(password, usuario.PasswordHash))
                    {
                        // Verificar que el usuario esté activo
                        if (usuario.Activo != true)
                        {
                            return (false, "Usuario inactivo. Contacte al administrador.", string.Empty);
                        }

                        // Establecer sesión de usuario
                        CurrentSession.Instance.LoginAsUsuario(
                            usuario.IdUsuario,
                            usuario.Nombre,
                            usuario.Apellido ?? string.Empty,
                            usuario.Email,
                            usuario.Rol
                        );

                        return (true, "Login exitoso", "ADMIN");
                    }
                }

                return (false, "Información incorrecta", string.Empty);
            }
            catch (Exception ex)
            {
                return (false, $"Error de servidor: {ex.Message}", string.Empty);
            }
        }

        public void Logout()
        {
            CurrentSession.Instance.Logout();
        }
    }
}
