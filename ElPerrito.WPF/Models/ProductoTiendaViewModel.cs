namespace ElPerrito.WPF.Models
{
    public class ProductoTiendaViewModel
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public decimal PrecioVenta { get; set; }
        public int Stock { get; set; }
        public string Imagen { get; set; } = "/images/placeholder.png";
        public bool Activo { get; set; }
    }
}
