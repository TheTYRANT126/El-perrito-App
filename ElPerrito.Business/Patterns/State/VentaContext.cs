using ElPerrito.Core.Logging;

namespace ElPerrito.Business.Patterns.State
{
    public class VentaContext
    {
        private readonly Logger _logger = Logger.Instance;
        private IEstadoVenta _estadoActual;
        public int IdVenta { get; set; }

        public VentaContext(int idVenta)
        {
            IdVenta = idVenta;
            _estadoActual = new EstadoPendiente();
        }

        public void SetEstado(IEstadoVenta nuevoEstado)
        {
            _logger.LogInfo($"Venta {IdVenta}: Cambiando estado a {nuevoEstado.ObtenerEstado()}");
            _estadoActual = nuevoEstado;
        }

        public string ObtenerEstado() => _estadoActual.ObtenerEstado();
        public void ProcesarPago() => _estadoActual.ProcesarPago(this);
        public void PrepararEnvio() => _estadoActual.PrepararEnvio(this);
        public void Enviar() => _estadoActual.Enviar(this);
        public void Entregar() => _estadoActual.Entregar(this);
        public void Cancelar() => _estadoActual.Cancelar(this);
    }
}
