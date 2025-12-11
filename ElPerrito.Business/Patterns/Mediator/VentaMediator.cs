using ElPerrito.Core.Logging;

namespace ElPerrito.Business.Patterns.Mediator
{
    /// <summary>
    /// Mediador concreto que coordina la comunicación entre componentes de venta
    /// </summary>
    public class VentaMediator : IMediator
    {
        private readonly Logger _logger = Logger.Instance;
        private readonly InventarioComponent _inventario;
        private readonly PagoComponent _pago;
        private readonly NotificacionComponent _notificacion;

        public VentaMediator(
            InventarioComponent inventario,
            PagoComponent pago,
            NotificacionComponent notificacion)
        {
            _inventario = inventario;
            _pago = pago;
            _notificacion = notificacion;

            _inventario.SetMediator(this);
            _pago.SetMediator(this);
            _notificacion.SetMediator(this);
        }

        public void Notify(IMediatorComponent sender, string eventName, object? data = null)
        {
            _logger.LogInfo($"Mediator: Evento '{eventName}' desde {sender.GetComponentName()}");

            switch (eventName)
            {
                case "PagoCompletado":
                    _inventario.ActualizarStock(data);
                    _notificacion.EnviarConfirmacion(data);
                    break;

                case "StockInsuficiente":
                    _notificacion.AlertarStockBajo(data);
                    break;

                case "InventarioActualizado":
                    _logger.LogInfo("Inventario actualizado correctamente");
                    break;
            }
        }
    }

    public class InventarioComponent : IMediatorComponent
    {
        private IMediator? _mediator;

        public void SetMediator(IMediator mediator) => _mediator = mediator;
        public string GetComponentName() => "Inventario";

        public void ActualizarStock(object? ventaData)
        {
            // Lógica de actualización
            _mediator?.Notify(this, "InventarioActualizado", ventaData);
        }
    }

    public class PagoComponent : IMediatorComponent
    {
        private IMediator? _mediator;

        public void SetMediator(IMediator mediator) => _mediator = mediator;
        public string GetComponentName() => "Pago";

        public void ProcesarPago(object ventaData)
        {
            // Lógica de pago
            _mediator?.Notify(this, "PagoCompletado", ventaData);
        }
    }

    public class NotificacionComponent : IMediatorComponent
    {
        private IMediator? _mediator;

        public void SetMediator(IMediator mediator) => _mediator = mediator;
        public string GetComponentName() => "Notificación";

        public void EnviarConfirmacion(object? ventaData)
        {
            System.Console.WriteLine("Enviando confirmación al cliente...");
        }

        public void AlertarStockBajo(object? productoData)
        {
            System.Console.WriteLine("Alertando stock bajo...");
        }
    }
}
