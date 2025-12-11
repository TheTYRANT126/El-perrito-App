using ElPerrito.Data.Entities;

namespace ElPerrito.Business.Builders
{
    /// <summary>
    /// Interfaz para el Builder de Venta
    /// </summary>
    public interface IVentaBuilder
    {
        IVentaBuilder SetCliente(int idCliente);
        IVentaBuilder SetTotal(decimal total);
        IVentaBuilder SetEstadoPago(string estadoPago);
        IVentaBuilder SetEstadoEnvio(string estadoEnvio);
        IVentaBuilder SetDireccionEnvio(string direccionEnvio);
        IVentaBuilder SetDireccionEnvioId(int? idDireccionEnvio);
        IVentaBuilder AddDetalle(int idProducto, int cantidad, decimal precioUnitario);
        Ventum Build();
        void Reset();
    }
}
