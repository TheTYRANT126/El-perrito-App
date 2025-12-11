using ElPerrito.Data.Repositories.Interfaces;
using ElPerrito.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElPerrito.Business.Patterns.Proxy
{
    public class ProductoServiceReal : IProductoService
    {
        private readonly IProductoRepository _repository;

        public ProductoServiceReal(IProductoRepository repository)
        {
            _repository = repository;
        }

        public async Task<Producto?> ObtenerProductoAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<List<Producto>> ObtenerTodosAsync()
        {
            var productos = await _repository.GetAllAsync();
            return productos.ToList();
        }
    }
}
