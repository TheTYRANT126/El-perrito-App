using System;
using System.Threading.Tasks;
using ElPerrito.Data.Repositories.Interfaces;

namespace ElPerrito.Data.UnitOfWork
{
    /// <summary>
    /// Interfaz para el patrón Unit of Work
    /// Coordina el trabajo de múltiples repositorios y mantiene una única transacción
    /// </summary>
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
