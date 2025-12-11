namespace ElPerrito.Business.Patterns.Decorator
{
    public class DescuentoDecorator : ProductoDecorator
    {
        private readonly decimal _porcentajeDescuento;

        public DescuentoDecorator(IProductoComponent producto, decimal porcentajeDescuento)
            : base(producto)
        {
            _porcentajeDescuento = porcentajeDescuento;
        }

        public override decimal ObtenerPrecio()
        {
            decimal precioBase = _producto.ObtenerPrecio();
            return precioBase - (precioBase * _porcentajeDescuento / 100m);
        }

        public override string ObtenerDescripcion()
        {
            return $"{_producto.ObtenerDescripcion()} - Descuento {_porcentajeDescuento}%";
        }
    }
}
