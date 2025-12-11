namespace ElPerrito.Business.Patterns.Decorator
{
    public class ProductoBase : IProductoComponent
    {
        private readonly string _nombre;
        private readonly decimal _precio;

        public ProductoBase(string nombre, decimal precio)
        {
            _nombre = nombre;
            _precio = precio;
        }

        public decimal ObtenerPrecio() => _precio;
        public string ObtenerDescripcion() => _nombre;
    }
}
