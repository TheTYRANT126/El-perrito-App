using ElPerrito.Core.Logging;

namespace ElPerrito.Business.Patterns.Memento
{
    /// <summary>
    /// Originator - Objeto que puede crear y restaurar mementos
    /// </summary>
    public class VentaOriginator
    {
        private readonly Logger _logger = Logger.Instance;
        public int IdVenta { get; set; }
        public string EstadoPago { get; set; } = string.Empty;
        public string EstadoEnvio { get; set; } = string.Empty;
        public decimal Total { get; set; }

        public VentaMemento CreateMemento()
        {
            _logger.LogInfo($"Creando snapshot de venta {IdVenta}");
            return new VentaMemento(IdVenta, EstadoPago, EstadoEnvio, Total);
        }

        public void RestoreMemento(VentaMemento memento)
        {
            _logger.LogInfo($"Restaurando venta {memento.IdVenta} desde snapshot {memento.FechaSnapshot}");
            IdVenta = memento.IdVenta;
            EstadoPago = memento.EstadoPago;
            EstadoEnvio = memento.EstadoEnvio;
            Total = memento.Total;
        }
    }
}
