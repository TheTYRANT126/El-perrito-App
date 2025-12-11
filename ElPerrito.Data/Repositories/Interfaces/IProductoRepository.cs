using System.Collections.Generic;
using System.Threading.Tasks;
using ElPerrito.Data.Entities;

namespace ElPerrito.Data.Repositories.Interfaces
{
    public interface IProductoRepository : IRepository<Producto>
    {
        Task<IEnumerable<Producto>> GetActiveProductsAsync();
        Task<IEnumerable<Producto>> GetProductsByCategoryAsync(int idCategoria);
        Task<IEnumerable<Producto>> SearchProductsAsync(string searchTerm);
        Task<Producto?> GetProductWithInventoryAsync(int id);
        Task<Producto?> GetProductWithImagesAsync(int id);
        Task<IEnumerable<Producto>> GetProductsWithLowStockAsync(int threshold = 10);
        Task<IEnumerable<Producto>> GetExpiringProductsAsync(int daysThreshold = 30);
        Task<bool> SoftDeleteAsync(int id);
        Task<bool> ActivateProductAsync(int id);
    }
}
