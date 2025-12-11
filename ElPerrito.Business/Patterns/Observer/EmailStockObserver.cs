using ElPerrito.Core.Logging;

namespace ElPerrito.Business.Patterns.Observer
{
    public class EmailStockObserver : IObserver<StockAlertData>
    {
        private readonly Logger _logger = Logger.Instance;

        public void Update(StockAlertData data)
        {
            _logger.LogWarning($"ALERTA STOCK BAJO - Producto: {data.NombreProducto} (ID: {data.IdProducto}) - Stock: {data.StockActual}/{data.StockMinimo}");

            // Aquí se enviaría un email real
            System.Console.WriteLine($"[EMAIL] Alerta de stock bajo para {data.NombreProducto}");
        }
    }
}
