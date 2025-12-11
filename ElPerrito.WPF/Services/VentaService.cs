using ElPerrito.Data.Context;
using ElPerrito.WPF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElPerrito.WPF.Services
{
    public class VentaService
    {
        private readonly ElPerritoContext _context;

        public VentaService()
        {
            _context = new ElPerritoContext();
        }

        public async Task<List<VentaViewModel>> ObtenerVentasAsync()
        {
            try
            {
                var ventasDb = await _context.Venta
                    .Include(v => v.IdClienteNavigation)
                    .OrderByDescending(v => v.Fecha)
                    .Take(100) // Limitar a las últimas 100 ventas
                    .ToListAsync();

                var ventas = ventasDb.Select(v => new VentaViewModel
                {
                    IdVenta = v.IdVenta,
                    NombreCliente = $"{v.IdClienteNavigation.Nombre} {v.IdClienteNavigation.Apellido}",
                    Fecha = v.Fecha,
                    Total = v.Total,
                    EstadoPago = CapitalizarEstado(v.EstadoPago)
                }).ToList();

                return ventas;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error al cargar ventas: {ex.Message}\n\nDetalles: {ex.InnerException?.Message}",
                                              "Error",
                                              System.Windows.MessageBoxButton.OK,
                                              System.Windows.MessageBoxImage.Error);
                return new List<VentaViewModel>();
            }
        }

        public async Task<(decimal totalHoy, decimal totalMes, int ordenesCompletas, int ordenesPendientes)> ObtenerEstadisticasAsync()
        {
            try
            {
                var hoy = DateTime.Now.Date;
                var inicioMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

                // Total de ventas de hoy
                var totalHoy = await _context.Venta
                    .Where(v => v.Fecha.Date == hoy)
                    .SumAsync(v => (decimal?)v.Total) ?? 0;

                // Total de ventas del mes
                var totalMes = await _context.Venta
                    .Where(v => v.Fecha >= inicioMes)
                    .SumAsync(v => (decimal?)v.Total) ?? 0;

                // Total de órdenes completas (pagadas)
                var ordenesCompletas = await _context.Venta
                    .CountAsync(v => v.EstadoPago == "pagado");

                // Total de órdenes pendientes
                var ordenesPendientes = await _context.Venta
                    .CountAsync(v => v.EstadoPago == "pendiente");

                return (totalHoy, totalMes, ordenesCompletas, ordenesPendientes);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error al cargar estadísticas: {ex.Message}\n\nDetalles: {ex.InnerException?.Message}",
                                              "Error",
                                              System.Windows.MessageBoxButton.OK,
                                              System.Windows.MessageBoxImage.Error);
                return (0, 0, 0, 0);
            }
        }

        private string CapitalizarEstado(string estado)
        {
            if (string.IsNullOrEmpty(estado)) return "";
            return char.ToUpper(estado[0]) + estado.Substring(1);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
