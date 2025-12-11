namespace ElPerrito.Business.Patterns.Decorator
{
    public abstract class ProductoDecorator : IProductoComponent
    {
        protected readonly IProductoComponent _producto;

        protected ProductoDecorator(IProductoComponent producto)
        {
            _producto = producto;
        }

        public virtual decimal ObtenerPrecio() => _producto.ObtenerPrecio();
        public virtual string ObtenerDescripcion() => _producto.ObtenerDescripcion();
    }
}
