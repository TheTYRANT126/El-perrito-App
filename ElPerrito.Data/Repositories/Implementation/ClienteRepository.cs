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
    public class ClienteRepository : BaseRepository<Cliente>, IClienteRepository
    {
        public ClienteRepository(ElPerritoContext context) : base(context)
        {
        }

        protected override string GetKeyPropertyName()
        {
            return nameof(Cliente.IdCliente);
        }

        protected override async Task BeforeAddAsync(Cliente entity)
        {
            entity.FechaRegistro = DateTime.Now;
            entity.Estado = "activo";
            await base.BeforeAddAsync(entity);
        }

        public async Task<Cliente?> GetByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return null;
            }

            return await FirstOrDefaultAsync(c => c.Email.ToLower() == email.ToLower());
        }

        public async Task<IEnumerable<Cliente>> GetActiveClientsAsync()
        {
            return await FindAsync(c => c.Estado == "activo");
        }

        public async Task<Cliente?> GetClientWithOrdersAsync(int id)
        {
            return await _dbSet
                .Include(c => c.Venta)
                    .ThenInclude(v => v.DetalleVenta)
                .FirstOrDefaultAsync(c => c.IdCliente == id);
        }

        public async Task<Cliente?> GetClientWithAddressesAsync(int id)
        {
            return await _dbSet
                .Include(c => c.Direccions)
                .Include(c => c.DireccionEnvios)
                .FirstOrDefaultAsync(c => c.IdCliente == id);
        }

        public async Task<IEnumerable<Ventum>> GetClientPurchaseHistoryAsync(int id)
        {
            return await _context.Venta
                .Include(v => v.DetalleVenta)
                    .ThenInclude(d => d.IdProductoNavigation)
                .Where(v => v.IdCliente == id)
                .OrderByDescending(v => v.Fecha)
                .ToListAsync();
        }

        public async Task<bool> EmailExistsAsync(string email, int? excludeClientId = null)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            var query = _dbSet.Where(c => c.Email.ToLower() == email.ToLower());

            if (excludeClientId.HasValue)
            {
                query = query.Where(c => c.IdCliente != excludeClientId.Value);
            }

            return await query.AnyAsync();
        }
    }
}
