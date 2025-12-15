using ElPerrito.Data.Entities;

namespace ElPerrito.Business.Patterns.Visitor
{
    public interface IVisitor
    {
        void Visit(Producto producto);
        void Visit(Cliente cliente);
        void Visit(Ventum venta);
    }
}
