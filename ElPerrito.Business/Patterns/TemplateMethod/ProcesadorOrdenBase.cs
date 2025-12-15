using ElPerrito.Core.Logging;

namespace ElPerrito.Business.Patterns.TemplateMethod
{
    public abstract class ProcesadorOrdenBase
    {
        protected readonly Logger _logger = Logger.Instance;

        // Template Method
        public void ProcesarOrden(int idVenta)
        {
            _logger.LogInfo($"Iniciando procesamiento de orden {idVenta}");

            ValidarOrden(idVenta);
            VerificarStock(idVenta);
            ProcesarPago(idVenta);
            ActualizarInventario(idVenta);
            GenerarDocumentos(idVenta);
            NotificarCliente(idVenta);

            _logger.LogInfo($"Orden {idVenta} procesada exitosamente");
        }

        // Métodos abstractos que deben implementar las subclases
        protected abstract void ValidarOrden(int idVenta);
        protected abstract void VerificarStock(int idVenta);
        protected abstract void ProcesarPago(int idVenta);
        protected abstract void ActualizarInventario(int idVenta);

        // Métodos con implementación por defecto (pueden ser sobrescritos)
        protected virtual void GenerarDocumentos(int idVenta)
        {
            _logger.LogInfo($"Generando documentos para orden {idVenta}");
        }

        protected virtual void NotificarCliente(int idVenta)
        {
            _logger.LogInfo($"Notificando cliente sobre orden {idVenta}");
        }
    }
}
