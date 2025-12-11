namespace ElPerrito.Business.Patterns.Decorator
{
    /// <summary>
    /// Patrón Decorator - Componente base
    /// </summary>
    public interface IProductoComponent
    {
        decimal ObtenerPrecio();
        string ObtenerDescripcion();
    }
}
