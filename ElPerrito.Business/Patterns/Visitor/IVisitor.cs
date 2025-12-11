using ElPerrito.Data.Entities;

namespace ElPerrito.Business.Patterns.Visitor
{
    /// <summary>
    /// Patrón Visitor - Visitante
    /// </summary>
    public interface IVisitor
    {
        void Visit(Producto producto);
        void Visit(Cliente cliente);
        void Visit(Ventum venta);
    }
}
