using System;
using System.Threading.Tasks;
using ElPerrito.Data.Repositories.Interfaces;

namespace ElPerrito.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        // Repositorios
        IProductoRepository Productos { get; }
        IUsuarioRepository Usuarios { get; }
        IClienteRepository Clientes { get; }
        ICarritoRepository Carritos { get; }
        IVentaRepository Ventas { get; }
        ICategoriaRepository Categorias { get; }

        // Operaciones de transacción
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
