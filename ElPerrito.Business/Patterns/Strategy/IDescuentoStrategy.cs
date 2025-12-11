namespace ElPerrito.Business.Patterns.Strategy
{
    /// <summary>
    /// Patrón Strategy para diferentes tipos de descuentos
    /// </summary>
    public interface IDescuentoStrategy
    {
        decimal CalcularDescuento(decimal montoOriginal);
        string ObtenerDescripcion();
    }
}
