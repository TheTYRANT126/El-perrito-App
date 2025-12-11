namespace ElPerrito.Domain.Enums
{
    /// <summary>
    /// Estados de pago de una venta
    /// </summary>
    public enum PaymentStatus
    {
        /// <summary>
        /// Pago pendiente
        /// </summary>
        Pendiente,

        /// <summary>
        /// Pago completado
        /// </summary>
        Completado,

        /// <summary>
        /// Pago cancelado
        /// </summary>
        Cancelado,

        /// <summary>
        /// Pago rechazado
        /// </summary>
        Rechazado,

        /// <summary>
        /// Pago reembolsado
        /// </summary>
        Reembolsado
    }
}
