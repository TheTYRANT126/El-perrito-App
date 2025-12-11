using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ElPerrito.Data.Entities;

namespace ElPerrito.Data.Repositories.Interfaces
{
    public interface IVentaRepository : IRepository<Ventum>
    {
        Task<Ventum?> GetSaleWithDetailsAsync(int id);
        Task<IEnumerable<Ventum>> GetSalesByClientAsync(int idCliente);
        Task<IEnumerable<Ventum>> GetSalesByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<Ventum>> GetSalesByStatusAsync(string estadoPago, string? estadoEnvio = null);
        Task<decimal> GetTotalSalesAmountAsync(DateTime? startDate = null, DateTime? endDate = null);
        Task<int> GetTotalSalesCountAsync(DateTime? startDate = null, DateTime? endDate = null);
        Task<bool> UpdatePaymentStatusAsync(int id, string estadoPago);
        Task<bool> UpdateShippingStatusAsync(int id, string estadoEnvio);
    }
}
