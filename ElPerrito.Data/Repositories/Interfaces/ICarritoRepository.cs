using System.Threading.Tasks;
using ElPerrito.Data.Entities;

namespace ElPerrito.Data.Repositories.Interfaces
{
    public interface ICarritoRepository : IRepository<Carrito>
    {
        Task<Carrito?> GetActiveCartByClientIdAsync(int idCliente);
        Task<Carrito?> GetCartWithDetailsAsync(int id);
        Task<bool> AddItemToCartAsync(int idCarrito, int idProducto, int cantidad, decimal precioUnitario);
        Task<bool> UpdateCartItemQuantityAsync(int idItem, int cantidad);
        Task<bool> RemoveCartItemAsync(int idItem);
        Task<decimal> GetCartTotalAsync(int idCarrito);
        Task<bool> MarkCartAsCompletedAsync(int idCarrito);
        Task<bool> MarkCartAsAbandonedAsync(int idCarrito);
        Task<Carrito?> CreateCartForClientAsync(int idCliente);
    }
}
