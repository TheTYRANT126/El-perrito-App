namespace ElPerrito.WPF.Models
{
    public class ProductoViewModel
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public decimal PrecioVenta { get; set; }
        public int Stock { get; set; }
        public bool Activo { get; set; }
    }
}
