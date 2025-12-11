namespace ElPerrito.Business.Patterns.TemplateMethod
{
    public class ProcesadorOrdenEstandar : ProcesadorOrdenBase
    {
        protected override void ValidarOrden(int idVenta)
        {
            _logger.LogInfo($"Validando orden estándar {idVenta}");
            // Lógica de validación estándar
        }

        protected override void VerificarStock(int idVenta)
        {
            _logger.LogInfo($"Verificando stock para orden {idVenta}");
            // Lógica de verificación de stock
        }

        protected override void ProcesarPago(int idVenta)
        {
            _logger.LogInfo($"Procesando pago para orden {idVenta}");
            // Lógica de procesamiento de pago
        }

        protected override void ActualizarInventario(int idVenta)
        {
            _logger.LogInfo($"Actualizando inventario para orden {idVenta}");
            // Lógica de actualización de inventario
        }
    }
}
