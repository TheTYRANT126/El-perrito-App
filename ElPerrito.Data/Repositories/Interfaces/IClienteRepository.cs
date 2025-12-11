using System.Collections.Generic;
using System.Threading.Tasks;
using ElPerrito.Data.Entities;

namespace ElPerrito.Data.Repositories.Interfaces
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Task<Cliente?> GetByEmailAsync(string email);
        Task<IEnumerable<Cliente>> GetActiveClientsAsync();
        Task<Cliente?> GetClientWithOrdersAsync(int id);
        Task<Cliente?> GetClientWithAddressesAsync(int id);
        Task<IEnumerable<Ventum>> GetClientPurchaseHistoryAsync(int id);
        Task<bool> EmailExistsAsync(string email, int? excludeClientId = null);
    }
}
