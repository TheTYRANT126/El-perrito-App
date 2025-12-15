using System;
using ElPerrito.Domain.Patterns;

namespace ElPerrito.Domain.Models
{
    public class ProductoDto : IPrototype<ProductoDto>
    {
        public int IdProducto { get; set; }
        public int IdCategoria { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public decimal PrecioVenta { get; set; }
        public string? Imagen { get; set; }
        public DateOnly? Caducidad { get; set; }
        public bool EsMedicamento { get; set; }
        public bool Activo { get; set; }
        public int Stock { get; set; }
        public int StockMinimo { get; set; }

        public ProductoDto Clone()
        {
            return (ProductoDto)this.MemberwiseClone();
        }

        public ProductoDto DeepClone()
        {
            return new ProductoDto
            {
                IdProducto = this.IdProducto,
                IdCategoria = this.IdCategoria,
                Nombre = string.Copy(this.Nombre),
                Descripcion = this.Descripcion != null ? string.Copy(this.Descripcion) : null,
                PrecioVenta = this.PrecioVenta,
                Imagen = this.Imagen != null ? string.Copy(this.Imagen) : null,
                Caducidad = this.Caducidad,
                EsMedicamento = this.EsMedicamento,
                Activo = this.Activo,
                Stock = this.Stock,
                StockMinimo = this.StockMinimo
            };
        }
    }
}
