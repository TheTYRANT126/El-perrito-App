using ElPerrito.Data.Entities;
using ElPerrito.Core.Logging;
using System.Collections.Generic;

namespace ElPerrito.Business.Patterns.Visitor
{
    public class ValidacionVisitor : IVisitor
    {
        private readonly Logger _logger = Logger.Instance;
        public List<string> Errores { get; } = new();

        public void Visit(Producto producto)
        {
            _logger.LogInfo($"Validando producto {producto.IdProducto}");

            if (string.IsNullOrWhiteSpace(producto.Nombre))
                Errores.Add("El nombre del producto es requerido");

            if (producto.PrecioVenta <= 0)
                Errores.Add("El precio debe ser mayor a 0");

            if (producto.IdCategoria <= 0)
                Errores.Add("Debe especificar una categoría válida");
        }

        public void Visit(Cliente cliente)
        {
            _logger.LogInfo($"Validando cliente {cliente.IdCliente}");

            if (string.IsNullOrWhiteSpace(cliente.Nombre))
                Errores.Add("El nombre del cliente es requerido");

            if (string.IsNullOrWhiteSpace(cliente.Email))
                Errores.Add("El email del cliente es requerido");
        }

        public void Visit(Ventum venta)
        {
            _logger.LogInfo($"Validando venta {venta.IdVenta}");

            if (venta.IdCliente <= 0)
                Errores.Add("Debe especificar un cliente válido");

            if (venta.Total <= 0)
                Errores.Add("El total debe ser mayor a 0");
        }
    }
}
