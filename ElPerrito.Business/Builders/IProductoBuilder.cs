using ElPerrito.Data.Entities;
using System;

namespace ElPerrito.Business.Builders
{
    /// <summary>
    /// Interfaz para el Builder de Producto
    /// </summary>
    public interface IProductoBuilder
    {
        IProductoBuilder SetNombre(string nombre);
        IProductoBuilder SetDescripcion(string descripcion);
        IProductoBuilder SetPrecio(decimal precio);
        IProductoBuilder SetCategoria(int idCategoria);
        IProductoBuilder SetImagen(string imagen);
        IProductoBuilder SetCaducidad(DateOnly? caducidad);
        IProductoBuilder SetEsMedicamento(bool esMedicamento);
        IProductoBuilder SetActivo(bool activo);
        IProductoBuilder SetUsuarioCreador(int? idUsuario);
        IProductoBuilder ConInventario(int stock, int stockMinimo);
        Producto Build();
        void Reset();
    }
}
