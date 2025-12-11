using ElPerrito.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElPerrito.Business.Patterns.Proxy
{
    /// <summary>
    /// Interfaz para el servicio de productos
    /// </summary>
    public interface IProductoService
    {
        Task<Producto?> ObtenerProductoAsync(int id);
        Task<List<Producto>> ObtenerTodosAsync();
    }
}
