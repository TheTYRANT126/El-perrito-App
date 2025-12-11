using System.Collections.Generic;
using System.Threading.Tasks;
using ElPerrito.Data.Entities;

namespace ElPerrito.Data.Repositories.Interfaces
{
    public interface ICategoriaRepository : IRepository<Categorium>
    {
        Task<IEnumerable<Categorium>> GetActiveCategoriesAsync();
        Task<Categorium?> GetCategoryWithProductsAsync(int id);
        Task<int> GetProductCountByCategoryAsync(int id);
        Task<bool> SoftDeleteAsync(int id);
        Task<bool> ActivateCategoryAsync(int id);
    }
}
