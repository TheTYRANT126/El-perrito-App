namespace ElPerrito.Business.Patterns.Decorator
{
    public class PromocionDecorator : ProductoDecorator
    {
        private readonly string _nombrePromocion;
        private readonly decimal _descuentoFijo;

        public PromocionDecorator(IProductoComponent producto, string nombrePromocion, decimal descuentoFijo)
            : base(producto)
        {
            _nombrePromocion = nombrePromocion;
            _descuentoFijo = descuentoFijo;
        }

        public override decimal ObtenerPrecio()
        {
            return _producto.ObtenerPrecio() - _descuentoFijo;
        }

        public override string ObtenerDescripcion()
        {
            return $"{_producto.ObtenerDescripcion()} - Promoción: {_nombrePromocion}";
        }
    }
}
