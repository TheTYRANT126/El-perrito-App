using System.Collections.Generic;
using System.Threading.Tasks;
using ElPerrito.Data.Entities;

namespace ElPerrito.Data.Repositories.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<Usuario?> GetByEmailAsync(string email);
        Task<IEnumerable<Usuario>> GetActiveUsersAsync();
        Task<IEnumerable<Usuario>> GetUsersByRoleAsync(string rol);
        Task<Usuario?> GetUserWithHistoryAsync(int id);
        Task<IEnumerable<HistorialUsuario>> GetUserChangesHistoryAsync(int id, int limit = 50);
        Task<bool> EmailExistsAsync(string email, int? excludeUserId = null);
        Task<bool> SoftDeleteAsync(int id);
        Task<bool> ActivateUserAsync(int id);
    }
}
