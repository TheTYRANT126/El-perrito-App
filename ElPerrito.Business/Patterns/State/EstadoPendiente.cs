using System;

namespace ElPerrito.Business.Patterns.State
{
    public class EstadoPendiente : IEstadoVenta
    {
        public void ProcesarPago(VentaContext context)
        {
            context.SetEstado(new EstadoPagado());
        }

        public void PrepararEnvio(VentaContext context)
        {
            throw new InvalidOperationException("Debe procesar el pago primero");
        }

        public void Enviar(VentaContext context)
        {
            throw new InvalidOperationException("Debe procesar el pago primero");
        }

        public void Entregar(VentaContext context)
        {
            throw new InvalidOperationException("Debe procesar el pago primero");
        }

        public void Cancelar(VentaContext context)
        {
            context.SetEstado(new EstadoCancelado());
        }

        public string ObtenerEstado() => "Pendiente";
    }

    public class EstadoPagado : IEstadoVenta
    {
        public void ProcesarPago(VentaContext context)
        {
            throw new InvalidOperationException("El pago ya fue procesado");
        }

        public void PrepararEnvio(VentaContext context)
        {
            context.SetEstado(new EstadoEnPreparacion());
        }

        public void Enviar(VentaContext context)
        {
            throw new InvalidOperationException("Debe preparar el envío primero");
        }

        public void Entregar(VentaContext context)
        {
            throw new InvalidOperationException("Debe preparar el envío primero");
        }

        public void Cancelar(VentaContext context)
        {
            context.SetEstado(new EstadoCancelado());
        }

        public string ObtenerEstado() => "Pagado";
    }

    public class EstadoEnPreparacion : IEstadoVenta
    {
        public void ProcesarPago(VentaContext context)
        {
            throw new InvalidOperationException("El pago ya fue procesado");
        }

        public void PrepararEnvio(VentaContext context)
        {
            throw new InvalidOperationException("Ya está en preparación");
        }

        public void Enviar(VentaContext context)
        {
            context.SetEstado(new EstadoEnviado());
        }

        public void Entregar(VentaContext context)
        {
            throw new InvalidOperationException("Debe enviar primero");
        }

        public void Cancelar(VentaContext context)
        {
            context.SetEstado(new EstadoCancelado());
        }

        public string ObtenerEstado() => "En Preparación";
    }

    public class EstadoEnviado : IEstadoVenta
    {
        public void ProcesarPago(VentaContext context)
        {
            throw new InvalidOperationException("El pago ya fue procesado");
        }

        public void PrepararEnvio(VentaContext context)
        {
            throw new InvalidOperationException("Ya fue enviado");
        }

        public void Enviar(VentaContext context)
        {
            throw new InvalidOperationException("Ya fue enviado");
        }

        public void Entregar(VentaContext context)
        {
            context.SetEstado(new EstadoEntregado());
        }

        public void Cancelar(VentaContext context)
        {
            throw new InvalidOperationException("No se puede cancelar un envío en tránsito");
        }

        public string ObtenerEstado() => "Enviado";
    }

    public class EstadoEntregado : IEstadoVenta
    {
        public void ProcesarPago(VentaContext context)
        {
            throw new InvalidOperationException("La venta ya fue completada");
        }

        public void PrepararEnvio(VentaContext context)
        {
            throw new InvalidOperationException("La venta ya fue completada");
        }

        public void Enviar(VentaContext context)
        {
            throw new InvalidOperationException("La venta ya fue completada");
        }

        public void Entregar(VentaContext context)
        {
            throw new InvalidOperationException("Ya fue entregado");
        }

        public void Cancelar(VentaContext context)
        {
            throw new InvalidOperationException("No se puede cancelar una venta entregada");
        }

        public string ObtenerEstado() => "Entregado";
    }

    public class EstadoCancelado : IEstadoVenta
    {
        public void ProcesarPago(VentaContext context)
        {
            throw new InvalidOperationException("La venta fue cancelada");
        }

        public void PrepararEnvio(VentaContext context)
        {
            throw new InvalidOperationException("La venta fue cancelada");
        }

        public void Enviar(VentaContext context)
        {
            throw new InvalidOperationException("La venta fue cancelada");
        }

        public void Entregar(VentaContext context)
        {
            throw new InvalidOperationException("La venta fue cancelada");
        }

        public void Cancelar(VentaContext context)
        {
            throw new InvalidOperationException("Ya está cancelada");
        }

        public string ObtenerEstado() => "Cancelado";
    }
}
