using ElPerrito.Data.Repositories.Interfaces;
using ElPerrito.Core.Logging;

namespace ElPerrito.Business.Patterns.Command
{
    public class ActualizarStockCommand : ICommand
    {
        private readonly IProductoRepository _repository;
        private readonly int _idProducto;
        private readonly int _nuevaCantidad;
        private int _cantidadAnterior;
        private readonly Logger _logger = Logger.Instance;

        public ActualizarStockCommand(IProductoRepository repository, int idProducto, int nuevaCantidad)
        {
            _repository = repository;
            _idProducto = idProducto;
            _nuevaCantidad = nuevaCantidad;
        }

        public void Execute()
        {
            var producto = _repository.GetByIdAsync(_idProducto).Result;
            if (producto?.Inventario != null)
            {
                _cantidadAnterior = producto.Inventario.Stock;
                producto.Inventario.Stock = _nuevaCantidad;
                _repository.UpdateAsync(producto).Wait();
                _logger.LogInfo($"Stock actualizado: Producto {_idProducto} - {_cantidadAnterior} -> {_nuevaCantidad}");
            }
        }

        public void Undo()
        {
            var producto = _repository.GetByIdAsync(_idProducto).Result;
            if (producto?.Inventario != null)
            {
                producto.Inventario.Stock = _cantidadAnterior;
                _repository.UpdateAsync(producto).Wait();
                _logger.LogInfo($"Stock revertido: Producto {_idProducto} - {_nuevaCantidad} -> {_cantidadAnterior}");
            }
        }

        public string GetDescription() => $"Actualizar stock del producto {_idProducto}";
    }
}
