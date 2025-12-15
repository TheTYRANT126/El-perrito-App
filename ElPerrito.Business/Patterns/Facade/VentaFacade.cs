using ElPerrito.Data.Repositories.Interfaces;
using ElPerrito.Business.Builders;
using ElPerrito.Business.Patterns.Observer;
using ElPerrito.Core.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using ElPerrito.Data.Entities;

namespace ElPerrito.Business.Patterns.Facade
{
    public class VentaFacade
    {
        private readonly IVentaRepository _ventaRepository;
        private readonly IProductoRepository _productoRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly StockSubject _stockSubject;
        private readonly Logger _logger = Logger.Instance;

        public VentaFacade(
            IVentaRepository ventaRepository,
            IProductoRepository productoRepository,
            IClienteRepository clienteRepository,
            StockSubject stockSubject)
        {
            _ventaRepository = ventaRepository;
            _productoRepository = productoRepository;
            _clienteRepository = clienteRepository;
            _stockSubject = stockSubject;
        }

        public async Task<(bool success, string message, int? idVenta)> CrearVentaCompletaAsync(
            int idCliente,
            string direccionEnvio,
            List<(int idProducto, int cantidad)> items)
        {
            _logger.LogInfo($"Iniciando creación de venta para cliente {idCliente}");

            try
            {
                // 1. Validar cliente
                var cliente = await _clienteRepository.GetByIdAsync(idCliente);
                if (cliente == null)
                    return (false, "Cliente no encontrado", null);

                // 2. Validar y obtener productos con precios
                var detalles = new List<(int idProducto, int cantidad, decimal precio)>();
                foreach (var item in items)
                {
                    var producto = await _productoRepository.GetByIdAsync(item.idProducto);
                    if (producto == null)
                        return (false, $"Producto {item.idProducto} no encontrado", null);

                    if (producto.Inventario == null || producto.Inventario.Stock < item.cantidad)
                        return (false, $"Stock insuficiente para {producto.Nombre}", null);

                    detalles.Add((item.idProducto, item.cantidad, producto.PrecioVenta));
                }

                // 3. Crear venta usando Builder
                var builder = new VentaBuilder();
                var venta = BuilderDirector.ConstructVentaCompleta(
                    builder,
                    idCliente,
                    direccionEnvio,
                    null,
                    detalles.ToArray()
                );

                // 4. Guardar venta
                await _ventaRepository.AddAsync(venta);

                // 5. Actualizar inventario y verificar stock bajo
                foreach (var item in items)
                {
                    var producto = await _productoRepository.GetByIdAsync(item.idProducto);
                    if (producto?.Inventario != null)
                    {
                        producto.Inventario.Stock -= item.cantidad;
                        await _productoRepository.UpdateAsync(producto);

                        // Notificar si stock bajo
                        _stockSubject.CheckStock(
                            producto.IdProducto,
                            producto.Nombre,
                            producto.Inventario.Stock,
                            producto.Inventario.StockMinimo
                        );
                    }
                }

                _logger.LogInfo($"Venta creada exitosamente: ID {venta.IdVenta}");
                return (true, "Venta creada exitosamente", venta.IdVenta);
            }
            catch (System.Exception ex)
            {
                _logger.LogError("Error al crear venta", ex);
                return (false, $"Error: {ex.Message}", null);
            }
        }
    }
}
