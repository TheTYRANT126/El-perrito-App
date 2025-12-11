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
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(ElPerritoContext context) : base(context)
        {
        }

        protected override string GetKeyPropertyName()
        {
            return nameof(Usuario.IdUsuario);
        }

        protected override async Task BeforeAddAsync(Usuario entity)
        {
            entity.FechaRegistro = DateTime.Now;
            entity.Activo = true;
            await base.BeforeAddAsync(entity);
        }

        protected override async Task BeforeUpdateAsync(Usuario entity)
        {
            entity.FechaUltimaModificacion = DateTime.Now;
            await base.BeforeUpdateAsync(entity);
        }

        public async Task<Usuario?> GetByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return null;
            }

            return await FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task<IEnumerable<Usuario>> GetActiveUsersAsync()
        {
            return await FindAsync(u => u.Activo == true);
        }

        public async Task<IEnumerable<Usuario>> GetUsersByRoleAsync(string rol)
        {
            if (string.IsNullOrWhiteSpace(rol))
            {
                return new List<Usuario>();
            }

            return await FindAsync(u => u.Rol == rol && u.Activo == true);
        }

        public async Task<Usuario?> GetUserWithHistoryAsync(int id)
        {
            return await _dbSet
                .Include(u => u.HistorialUsuarioIdUsuarioNavigations)
                    .ThenInclude(h => h.IdUsuarioModificadorNavigation)
                .FirstOrDefaultAsync(u => u.IdUsuario == id);
        }

        public async Task<IEnumerable<HistorialUsuario>> GetUserChangesHistoryAsync(int id, int limit = 50)
        {
            return await _context.HistorialUsuarios
                .Include(h => h.IdUsuarioModificadorNavigation)
                .Where(h => h.IdUsuario == id)
                .OrderByDescending(h => h.FechaCambio)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<bool> EmailExistsAsync(string email, int? excludeUserId = null)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            var query = _dbSet.Where(u => u.Email.ToLower() == email.ToLower());

            if (excludeUserId.HasValue)
            {
                query = query.Where(u => u.IdUsuario != excludeUserId.Value);
            }

            return await query.AnyAsync();
        }

        public async Task<bool> SoftDeleteAsync(int id)
        {
            var user = await GetByIdAsync(id);
            if (user == null)
            {
                return false;
            }

            user.Activo = false;
            user.FechaUltimaModificacion = DateTime.Now;
            await UpdateAsync(user);

            return true;
        }

        public async Task<bool> ActivateUserAsync(int id)
        {
            var user = await GetByIdAsync(id);
            if (user == null)
            {
                return false;
            }

            user.Activo = true;
            user.FechaUltimaModificacion = DateTime.Now;
            await UpdateAsync(user);

            return true;
        }
    }
}
