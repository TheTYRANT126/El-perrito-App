using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ElPerrito.Data.Context;
using ElPerrito.Data.Entities;
using ElPerrito.Data.Repositories.Interfaces;

namespace ElPerrito.Data.Repositories.Implementation
{
    public class ProductoRepository : BaseRepository<Producto>, IProductoRepository
    {
        public ProductoRepository(ElPerritoContext context) : base(context)
        {
        }

        protected override string GetKeyPropertyName()
        {
            return nameof(Producto.IdProducto);
        }

        protected override IQueryable<Producto> ApplyIncludes(IQueryable<Producto> query)
        {
            return query
                .Include(p => p.IdCategoriaNavigation)
                .Include(p => p.Inventario);
        }

        protected override async Task BeforeAddAsync(Producto entity)
        {
            entity.FechaCreacion = DateTime.Now;
            entity.Activo = true;
            await base.BeforeAddAsync(entity);
        }

        protected override async Task BeforeUpdateAsync(Producto entity)
        {
            entity.FechaModificacion = DateTime.Now;
            await base.BeforeUpdateAsync(entity);
        }

        public async Task<IEnumerable<Producto>> GetActiveProductsAsync()
        {
            return await FindAsync(p => p.Activo == true);
        }

        public async Task<IEnumerable<Producto>> GetProductsByCategoryAsync(int idCategoria)
        {
            return await FindAsync(p => p.IdCategoria == idCategoria && p.Activo == true);
        }

        public async Task<IEnumerable<Producto>> SearchProductsAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return await GetActiveProductsAsync();
            }

            searchTerm = searchTerm.ToLower();
            return await FindAsync(p =>
                p.Activo == true &&
                (p.Nombre.ToLower().Contains(searchTerm) ||
                 (p.Descripcion != null && p.Descripcion.ToLower().Contains(searchTerm))));
        }

        public async Task<Producto?> GetProductWithInventoryAsync(int id)
        {
            return await _dbSet
                .Include(p => p.Inventario)
                .Include(p => p.IdCategoriaNavigation)
                .FirstOrDefaultAsync(p => p.IdProducto == id);
        }

        public async Task<Producto?> GetProductWithImagesAsync(int id)
        {
            return await _dbSet
                .Include(p => p.ProductoImagens)
                .Include(p => p.IdCategoriaNavigation)
                .FirstOrDefaultAsync(p => p.IdProducto == id);
        }

        public async Task<IEnumerable<Producto>> GetProductsWithLowStockAsync(int threshold = 10)
        {
            return await _dbSet
                .Include(p => p.Inventario)
                .Include(p => p.IdCategoriaNavigation)
                .Where(p => p.Activo == true &&
                           p.Inventario != null &&
                           p.Inventario.Stock <= threshold)
                .ToListAsync();
        }

        public async Task<IEnumerable<Producto>> GetExpiringProductsAsync(int daysThreshold = 30)
        {
            var thresholdDate = DateOnly.FromDateTime(DateTime.Now.AddDays(daysThreshold));

            return await _dbSet
                .Include(p => p.Inventario)
                .Include(p => p.IdCategoriaNavigation)
                .Where(p => p.Activo == true &&
                           p.Caducidad.HasValue &&
                           p.Caducidad.Value <= thresholdDate)
                .OrderBy(p => p.Caducidad)
                .ToListAsync();
        }

        public async Task<bool> SoftDeleteAsync(int id)
        {
            var product = await GetByIdAsync(id);
            if (product == null)
            {
                return false;
            }

            product.Activo = false;
            product.FechaModificacion = DateTime.Now;
            await UpdateAsync(product);

            return true;
        }

        public async Task<bool> ActivateProductAsync(int id)
        {
            var product = await GetByIdAsync(id);
            if (product == null)
            {
                return false;
            }

            product.Activo = true;
            product.FechaModificacion = DateTime.Now;
            await UpdateAsync(product);

            return true;
        }
    }
}
