using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ElPerrito.Data.Context;
using ElPerrito.Data.Entities;
using ElPerrito.Data.Repositories.Interfaces;

namespace ElPerrito.Data.Repositories.Implementation
{
    public class CategoriaRepository : BaseRepository<Categorium>, ICategoriaRepository
    {
        public CategoriaRepository(ElPerritoContext context) : base(context)
        {
        }

        protected override string GetKeyPropertyName()
        {
            return nameof(Categorium.IdCategoria);
        }

        protected override async Task BeforeAddAsync(Categorium entity)
        {
            entity.Activa = true;
            await base.BeforeAddAsync(entity);
        }

        public async Task<IEnumerable<Categorium>> GetActiveCategoriesAsync()
        {
            return await FindAsync(c => c.Activa == true);
        }

        public async Task<Categorium?> GetCategoryWithProductsAsync(int id)
        {
            return await _dbSet
                .Include(c => c.Productos.Where(p => p.Activo == true))
                .FirstOrDefaultAsync(c => c.IdCategoria == id);
        }

        public async Task<int> GetProductCountByCategoryAsync(int id)
        {
            var category = await _dbSet
                .Include(c => c.Productos)
                .FirstOrDefaultAsync(c => c.IdCategoria == id);

            if (category == null)
            {
                return 0;
            }

            return category.Productos.Count(p => p.Activo == true);
        }

        public async Task<bool> SoftDeleteAsync(int id)
        {
            var category = await GetByIdAsync(id);
            if (category == null)
            {
                return false;
            }

            category.Activa = false;
            await UpdateAsync(category);

            return true;
        }

        public async Task<bool> ActivateCategoryAsync(int id)
        {
            var category = await GetByIdAsync(id);
            if (category == null)
            {
                return false;
            }

            category.Activa = true;
            await UpdateAsync(category);

            return true;
        }
    }
}
