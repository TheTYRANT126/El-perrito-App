namespace ElPerrito.Domain.Enums
{
    /// <summary>
    /// Estados posibles de un carrito de compras
    /// </summary>
    public enum CartStatus
    {
        /// <summary>
        /// Carrito activo, en uso
        /// </summary>
        Activo,

        /// <summary>
        /// Carrito abandonado por el cliente
        /// </summary>
        Abandonado,

        /// <summary>
        /// Carrito procesado y convertido en venta
        /// </summary>
        Comprado,

        /// <summary>
        /// Carrito cerrado sin completar compra
        /// </summary>
        Cerrado
    }
}
