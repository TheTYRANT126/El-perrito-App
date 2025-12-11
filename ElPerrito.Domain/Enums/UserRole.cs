namespace ElPerrito.Domain.Enums
{
    /// <summary>
    /// Roles de usuario en el sistema
    /// </summary>
    public enum UserRole
    {
        /// <summary>
        /// Administrador con acceso completo
        /// </summary>
        Admin,

        /// <summary>
        /// Operador con acceso limitado (solo productos)
        /// </summary>
        Operador
    }
}
