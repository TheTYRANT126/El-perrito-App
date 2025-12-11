using System;

namespace ElPerrito.Business.Patterns.Memento
{
    /// <summary>
    /// Patrón Memento - Guarda el estado de una venta
    /// </summary>
    public class VentaMemento
    {
        public int IdVenta { get; }
        public string EstadoPago { get; }
        public string EstadoEnvio { get; }
        public decimal Total { get; }
        public DateTime FechaSnapshot { get; }

        public VentaMemento(int idVenta, string estadoPago, string estadoEnvio, decimal total)
        {
            IdVenta = idVenta;
            EstadoPago = estadoPago;
            EstadoEnvio = estadoEnvio;
            Total = total;
            FechaSnapshot = DateTime.Now;
        }
    }
}
