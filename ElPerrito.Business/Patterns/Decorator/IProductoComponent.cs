namespace ElPerrito.Business.Patterns.Decorator
{
    public interface IProductoComponent
    {
        decimal ObtenerPrecio();
        string ObtenerDescripcion();
    }
}
