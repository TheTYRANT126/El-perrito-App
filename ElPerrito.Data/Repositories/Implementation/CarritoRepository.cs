using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ElPerrito.Data.Context;
using ElPerrito.Data.Entities;
using ElPerrito.Data.Repositories.Interfaces;

namespace ElPerrito.Data.Repositories.Implementation
{
    public class CarritoRepository : BaseRepository<Carrito>, ICarritoRepository
    {
        public CarritoRepository(ElPerritoContext context) : base(context)
        {
        }

        protected override string GetKeyPropertyName()
        {
            return nameof(Carrito.IdCarrito);
        }

        protected override IQueryable<Carrito> ApplyIncludes(IQueryable<Carrito> query)
        {
            return query
                .Include(c => c.DetalleCarritos)
                    .ThenInclude(d => d.IdProductoNavigation);
        }

        protected override async Task BeforeAddAsync(Carrito entity)
        {
            entity.FechaCreacion = DateTime.Now;
            entity.Estado = "activo";
            await base.BeforeAddAsync(entity);
        }

        public async Task<Carrito?> GetActiveCartByClientIdAsync(int idCliente)
        {
            return await FirstOrDefaultAsync(c => c.IdCliente == idCliente && c.Estado == "activo");
        }

        public async Task<Carrito?> GetCartWithDetailsAsync(int id)
        {
            return await _dbSet
                .Include(c => c.DetalleCarritos)
                    .ThenInclude(d => d.IdProductoNavigation)
                        .ThenInclude(p => p.Inventario)
                .FirstOrDefaultAsync(c => c.IdCarrito == id);
        }

        public async Task<bool> AddItemToCartAsync(int idCarrito, int idProducto, int cantidad, decimal precioUnitario)
        {
            // Buscar si el producto ya existe en el carrito
            var existingItem = await _context.DetalleCarritos
                .FirstOrDefaultAsync(d => d.IdCarrito == idCarrito && d.IdProducto == idProducto);

            if (existingItem != null)
            {
                // Si ya existe, actualizar la cantidad
                existingItem.Cantidad += cantidad;
                _context.DetalleCarritos.Update(existingItem);
            }
            else
            {
                // Si no existe, agregar nuevo item
                var newItem = new DetalleCarrito
                {
                    IdCarrito = idCarrito,
                    IdProducto = idProducto,
                    Cantidad = cantidad,
                    PrecioUnitario = precioUnitario
                };
                await _context.DetalleCarritos.AddAsync(newItem);
            }

            // Actualizar fecha de actualización del carrito
            var cart = await GetByIdAsync(idCarrito);
            if (cart != null)
            {
                await UpdateAsync(cart);
            }

            return true;
        }

        public async Task<bool> UpdateCartItemQuantityAsync(int idItem, int cantidad)
        {
            var item = await _context.DetalleCarritos.FindAsync(idItem);
            if (item == null)
            {
                return false;
            }

            if (cantidad <= 0)
            {
                _context.DetalleCarritos.Remove(item);
            }
            else
            {
                item.Cantidad = cantidad;
                _context.DetalleCarritos.Update(item);
            }

            // Actualizar fecha del carrito
            var cart = await GetByIdAsync(item.IdCarrito);
            if (cart != null)
            {
                await UpdateAsync(cart);
            }

            return true;
        }

        public async Task<bool> RemoveCartItemAsync(int idItem)
        {
            var item = await _context.DetalleCarritos.FindAsync(idItem);
            if (item == null)
            {
                return false;
            }

            var idCarrito = item.IdCarrito;
            _context.DetalleCarritos.Remove(item);

            // Actualizar fecha del carrito
            var cart = await GetByIdAsync(idCarrito);
            if (cart != null)
            {
                await UpdateAsync(cart);
            }

            return true;
        }

        public async Task<decimal> GetCartTotalAsync(int idCarrito)
        {
            var cart = await GetCartWithDetailsAsync(idCarrito);
            if (cart == null || !cart.DetalleCarritos.Any())
            {
                return 0;
            }

            return cart.DetalleCarritos.Sum(d => d.Cantidad * d.PrecioUnitario);
        }

        public async Task<bool> MarkCartAsCompletedAsync(int idCarrito)
        {
            var cart = await GetByIdAsync(idCarrito);
            if (cart == null)
            {
                return false;
            }

            cart.Estado = "comprado";
            await UpdateAsync(cart);

            return true;
        }

        public async Task<bool> MarkCartAsAbandonedAsync(int idCarrito)
        {
            var cart = await GetByIdAsync(idCarrito);
            if (cart == null)
            {
                return false;
            }

            cart.Estado = "abandonado";
            await UpdateAsync(cart);

            return true;
        }

        public async Task<Carrito?> CreateCartForClientAsync(int idCliente)
        {
            var newCart = new Carrito
            {
                IdCliente = idCliente,
                Estado = "activo",
                FechaCreacion = DateTime.Now
            };

            return await AddAsync(newCart);
        }
    }
}
