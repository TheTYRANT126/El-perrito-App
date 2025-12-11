using System;
using ElPerrito.Data.Entities;

namespace ElPerrito.Business.Builders
{
    /// <summary>
    /// Implementación del patrón Builder para construir objetos Producto complejos
    /// </summary>
    public class ProductoBuilder : IProductoBuilder
    {
        private Producto _producto;
        private Inventario? _inventario;

        public ProductoBuilder()
        {
            Reset();
        }

        public void Reset()
        {
            _producto = new Producto
            {
                FechaCreacion = DateTime.Now,
                Activo = true,
                EsMedicamento = false
            };
            _inventario = null;
        }

        public IProductoBuilder SetNombre(string nombre)
        {
            _producto.Nombre = nombre;
            return this;
        }

        public IProductoBuilder SetDescripcion(string descripcion)
        {
            _producto.Descripcion = descripcion;
            return this;
        }

        public IProductoBuilder SetPrecio(decimal precio)
        {
            if (precio <= 0)
                throw new ArgumentException("El precio debe ser mayor a 0");

            _producto.PrecioVenta = precio;
            return this;
        }

        public IProductoBuilder SetCategoria(int idCategoria)
        {
            if (idCategoria <= 0)
                throw new ArgumentException("Debe especificar una categoría válida");

            _producto.IdCategoria = idCategoria;
            return this;
        }

        public IProductoBuilder SetImagen(string imagen)
        {
            _producto.Imagen = imagen;
            return this;
        }

        public IProductoBuilder SetCaducidad(DateOnly? caducidad)
        {
            _producto.Caducidad = caducidad;
            return this;
        }

        public IProductoBuilder SetEsMedicamento(bool esMedicamento)
        {
            _producto.EsMedicamento = esMedicamento;
            return this;
        }

        public IProductoBuilder SetActivo(bool activo)
        {
            _producto.Activo = activo;
            return this;
        }

        public IProductoBuilder SetUsuarioCreador(int? idUsuario)
        {
            _producto.IdUsuarioCreador = idUsuario;
            return this;
        }

        public IProductoBuilder ConInventario(int stock, int stockMinimo)
        {
            if (stock < 0)
                throw new ArgumentException("El stock no puede ser negativo");

            if (stockMinimo < 0)
                throw new ArgumentException("El stock mínimo no puede ser negativo");

            _inventario = new Inventario
            {
                Stock = stock,
                StockMinimo = stockMinimo
            };
            return this;
        }

        public Producto Build()
        {
            // Validaciones
            if (string.IsNullOrWhiteSpace(_producto.Nombre))
                throw new InvalidOperationException("Debe especificar un nombre para el producto");

            if (_producto.IdCategoria <= 0)
                throw new InvalidOperationException("Debe especificar una categoría válida");

            if (_producto.PrecioVenta <= 0)
                throw new InvalidOperationException("Debe especificar un precio válido");

            // Asignar inventario si se especificó
            if (_inventario != null)
            {
                _producto.Inventario = _inventario;
            }

            var result = _producto;
            Reset(); // Limpiar para siguiente uso
            return result;
        }
    }
}
