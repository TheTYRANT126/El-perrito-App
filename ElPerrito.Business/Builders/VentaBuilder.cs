using System;
using System.Collections.Generic;
using ElPerrito.Data.Entities;

namespace ElPerrito.Business.Builders
{
    public class VentaBuilder : IVentaBuilder
    {
        private Ventum _venta;
        private List<DetalleVentum> _detalles;

        public VentaBuilder()
        {
            Reset();
        }

        public void Reset()
        {
            _venta = new Ventum
            {
                Fecha = DateTime.Now,
                EstadoPago = "pendiente",
                EstadoEnvio = "pendiente",
                Total = 0
            };
            _detalles = new List<DetalleVentum>();
        }

        public IVentaBuilder SetCliente(int idCliente)
        {
            _venta.IdCliente = idCliente;
            return this;
        }

        public IVentaBuilder SetTotal(decimal total)
        {
            _venta.Total = total;
            return this;
        }

        public IVentaBuilder SetEstadoPago(string estadoPago)
        {
            _venta.EstadoPago = estadoPago;
            return this;
        }

        public IVentaBuilder SetEstadoEnvio(string estadoEnvio)
        {
            _venta.EstadoEnvio = estadoEnvio;
            return this;
        }

        public IVentaBuilder SetDireccionEnvio(string direccionEnvio)
        {
            _venta.DireccionEnvio = direccionEnvio;
            return this;
        }

        public IVentaBuilder SetDireccionEnvioId(int? idDireccionEnvio)
        {
            _venta.IdDireccionEnvio = idDireccionEnvio;
            return this;
        }

        public IVentaBuilder AddDetalle(int idProducto, int cantidad, decimal precioUnitario)
        {
            var detalle = new DetalleVentum
            {
                IdProducto = idProducto,
                Cantidad = cantidad,
                PrecioUnitario = precioUnitario
            };
            _detalles.Add(detalle);
            return this;
        }

        public Ventum Build()
        {
            // Calcular total si no se especificó
            if (_venta.Total == 0 && _detalles.Count > 0)
            {
                decimal total = 0;
                foreach (var detalle in _detalles)
                {
                    total += detalle.Cantidad * detalle.PrecioUnitario;
                }
                _venta.Total = total;
            }

            // Asignar detalles
            _venta.DetalleVenta = _detalles;

            // Validaciones
            if (_venta.IdCliente <= 0)
                throw new InvalidOperationException("Debe especificar un cliente válido");

            if (_detalles.Count == 0)
                throw new InvalidOperationException("Debe agregar al menos un detalle a la venta");

            var result = _venta;
            Reset(); // Limpiar para siguiente uso
            return result;
        }
    }
}
