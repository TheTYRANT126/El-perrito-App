namespace ElPerrito.Business.Patterns.State
{
    /// <summary>
    /// Patrón State para estados de venta
    /// </summary>
    public interface IEstadoVenta
    {
        void ProcesarPago(VentaContext context);
        void PrepararEnvio(VentaContext context);
        void Enviar(VentaContext context);
        void Entregar(VentaContext context);
        void Cancelar(VentaContext context);
        string ObtenerEstado();
    }
}
