namespace ElPerrito.Domain.Enums
{
    /// <summary>
    /// Tipos de acciones para el registro de actividad
    /// </summary>
    public enum ActionType
    {
        /// <summary>
        /// Crear un nuevo registro
        /// </summary>
        Crear,

        /// <summary>
        /// Editar un registro existente
        /// </summary>
        Editar,

        /// <summary>
        /// Eliminar un registro
        /// </summary>
        Eliminar,

        /// <summary>
        /// Login de usuario
        /// </summary>
        Login,

        /// <summary>
        /// Logout de usuario
        /// </summary>
        Logout,

        /// <summary>
        /// Visualización de datos
        /// </summary>
        Ver,

        /// <summary>
        /// Exportación de datos
        /// </summary>
        Exportar,

        /// <summary>
        /// Importación de datos
        /// </summary>
        Importar,

        /// <summary>
        /// Otra acción
        /// </summary>
        Otro
    }
}
