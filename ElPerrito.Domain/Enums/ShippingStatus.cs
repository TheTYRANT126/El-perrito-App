namespace ElPerrito.Domain.Enums
{
    /// <summary>
    /// Estados de envío de una venta
    /// </summary>
    public enum ShippingStatus
    {
        /// <summary>
        /// Envío pendiente
        /// </summary>
        Pendiente,

        /// <summary>
        /// En preparación
        /// </summary>
        Preparando,

        /// <summary>
        /// Enviado
        /// </summary>
        Enviado,

        /// <summary>
        /// En tránsito
        /// </summary>
        EnTransito,

        /// <summary>
        /// Entregado
        /// </summary>
        Entregado,

        /// <summary>
        /// Cancelado
        /// </summary>
        Cancelado,

        /// <summary>
        /// Devuelto
        /// </summary>
        Devuelto
    }
}
