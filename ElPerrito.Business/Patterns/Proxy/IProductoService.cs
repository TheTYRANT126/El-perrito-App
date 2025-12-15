using ElPerrito.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElPerrito.Business.Patterns.Proxy
{
    public interface IProductoService
    {
        Task<Producto?> ObtenerProductoAsync(int id);
        Task<List<Producto>> ObtenerTodosAsync();
    }
}
