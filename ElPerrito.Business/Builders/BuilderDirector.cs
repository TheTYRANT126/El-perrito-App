using ElPerrito.Data.Entities;
using System;

namespace ElPerrito.Business.Builders
{
    /// <summary>
    /// Director que coordina la construcción de objetos complejos
    /// Parte del patrón Builder
    /// </summary>
    public class BuilderDirector
    {
        /// <summary>
        /// Construye una venta básica
        /// </summary>
        public static Ventum ConstructVentaBasica(IVentaBuilder builder, int idCliente, params (int idProducto, int cantidad, decimal precio)[] items)
        {
            builder.Reset();
            builder.SetCliente(idCliente);

            foreach (var item in items)
            {
                builder.AddDetalle(item.idProducto, item.cantidad, item.precio);
            }

            return builder.Build();
        }

        /// <summary>
        /// Construye una venta completa con todos los datos
        /// </summary>
        public static Ventum ConstructVentaCompleta(
            IVentaBuilder builder,
            int idCliente,
            string direccionEnvio,
            int? idDireccionEnvio,
            params (int idProducto, int cantidad, decimal precio)[] items)
        {
            builder.Reset();
            builder
                .SetCliente(idCliente)
                .SetDireccionEnvio(direccionEnvio)
                .SetDireccionEnvioId(idDireccionEnvio)
                .SetEstadoPago("pendiente")
                .SetEstadoEnvio("pendiente");

            foreach (var item in items)
            {
                builder.AddDetalle(item.idProducto, item.cantidad, item.precio);
            }

            return builder.Build();
        }

        /// <summary>
        /// Construye un producto básico
        /// </summary>
        public static Producto ConstructProductoBasico(
            IProductoBuilder builder,
            string nombre,
            decimal precio,
            int idCategoria)
        {
            builder.Reset();
            return builder
                .SetNombre(nombre)
                .SetPrecio(precio)
                .SetCategoria(idCategoria)
                .Build();
        }

        /// <summary>
        /// Construye un producto completo con inventario
        /// </summary>
        public static Producto ConstructProductoCompleto(
            IProductoBuilder builder,
            string nombre,
            string descripcion,
            decimal precio,
            int idCategoria,
            int stock,
            int stockMinimo,
            int? idUsuarioCreador = null)
        {
            builder.Reset();
            return builder
                .SetNombre(nombre)
                .SetDescripcion(descripcion)
                .SetPrecio(precio)
                .SetCategoria(idCategoria)
                .SetUsuarioCreador(idUsuarioCreador)
                .ConInventario(stock, stockMinimo)
                .Build();
        }

        /// <summary>
        /// Construye un producto medicamento
        /// </summary>
        public static Producto ConstructProductoMedicamento(
            IProductoBuilder builder,
            string nombre,
            string descripcion,
            decimal precio,
            int idCategoria,
            DateOnly caducidad,
            int stock,
            int stockMinimo)
        {
            builder.Reset();
            return builder
                .SetNombre(nombre)
                .SetDescripcion(descripcion)
                .SetPrecio(precio)
                .SetCategoria(idCategoria)
                .SetCaducidad(caducidad)
                .SetEsMedicamento(true)
                .ConInventario(stock, stockMinimo)
                .Build();
        }
    }
}
