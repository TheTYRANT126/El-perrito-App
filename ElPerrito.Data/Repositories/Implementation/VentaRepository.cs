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
    public class VentaRepository : BaseRepository<Ventum>, IVentaRepository
    {
        public VentaRepository(ElPerritoContext context) : base(context)
        {
        }

        protected override string GetKeyPropertyName()
        {
            return nameof(Ventum.IdVenta);
        }

        protected override IQueryable<Ventum> ApplyIncludes(IQueryable<Ventum> query)
        {
            return query
                .Include(v => v.DetalleVenta)
                    .ThenInclude(d => d.IdProductoNavigation)
                .Include(v => v.IdClienteNavigation);
        }

        protected override async Task BeforeAddAsync(Ventum entity)
        {
            entity.Fecha = DateTime.Now;
            entity.EstadoPago = entity.EstadoPago ?? "pendiente";
            entity.EstadoEnvio = entity.EstadoEnvio ?? "pendiente";
            await base.BeforeAddAsync(entity);
        }

        public async Task<Ventum?> GetSaleWithDetailsAsync(int id)
        {
            return await _dbSet
                .Include(v => v.DetalleVenta)
                    .ThenInclude(d => d.IdProductoNavigation)
                .Include(v => v.IdClienteNavigation)
                .Include(v => v.Envios)
                .Include(v => v.Pagos)
                .FirstOrDefaultAsync(v => v.IdVenta == id);
        }

        public async Task<IEnumerable<Ventum>> GetSalesByClientAsync(int idCliente)
        {
            return await FindAsync(v => v.IdCliente == idCliente);
        }

        public async Task<IEnumerable<Ventum>> GetSalesByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _dbSet
                .Include(v => v.DetalleVenta)
                .Include(v => v.IdClienteNavigation)
                .Where(v => v.Fecha >= startDate && v.Fecha <= endDate)
                .OrderByDescending(v => v.Fecha)
                .ToListAsync();
        }

        public async Task<IEnumerable<Ventum>> GetSalesByStatusAsync(string estadoPago, string? estadoEnvio = null)
        {
            var query = _dbSet
                .Include(v => v.DetalleVenta)
                .Include(v => v.IdClienteNavigation)
                .Where(v => v.EstadoPago == estadoPago);

            if (!string.IsNullOrWhiteSpace(estadoEnvio))
            {
                query = query.Where(v => v.EstadoEnvio == estadoEnvio);
            }

            return await query.OrderByDescending(v => v.Fecha).ToListAsync();
        }

        public async Task<decimal> GetTotalSalesAmountAsync(DateTime? startDate = null, DateTime? endDate = null)
        {
            var query = _dbSet.AsQueryable();

            if (startDate.HasValue)
            {
                query = query.Where(v => v.Fecha >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(v => v.Fecha <= endDate.Value);
            }

            return await query.SumAsync(v => v.Total);
        }

        public async Task<int> GetTotalSalesCountAsync(DateTime? startDate = null, DateTime? endDate = null)
        {
            var query = _dbSet.AsQueryable();

            if (startDate.HasValue)
            {
                query = query.Where(v => v.Fecha >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(v => v.Fecha <= endDate.Value);
            }

            return await query.CountAsync();
        }

        public async Task<bool> UpdatePaymentStatusAsync(int id, string estadoPago)
        {
            var sale = await GetByIdAsync(id);
            if (sale == null)
            {
                return false;
            }

            sale.EstadoPago = estadoPago;
            await UpdateAsync(sale);

            return true;
        }

        public async Task<bool> UpdateShippingStatusAsync(int id, string estadoEnvio)
        {
            var sale = await GetByIdAsync(id);
            if (sale == null)
            {
                return false;
            }

            sale.EstadoEnvio = estadoEnvio;
            await UpdateAsync(sale);

            return true;
        }
    }
}
