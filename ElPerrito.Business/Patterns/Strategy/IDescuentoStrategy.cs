namespace ElPerrito.Business.Patterns.Strategy
{
    public interface IDescuentoStrategy
    {
        decimal CalcularDescuento(decimal montoOriginal);
        string ObtenerDescripcion();
    }
}
