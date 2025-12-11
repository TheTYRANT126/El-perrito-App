using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using ElPerrito.Data.Context;
using ElPerrito.Data.Repositories.Interfaces;
using ElPerrito.Data.Repositories.Implementation;

namespace ElPerrito.Data.UnitOfWork
{
    /// <summary>
    /// Implementación del patrón Unit of Work
    /// Coordina el trabajo de múltiples repositorios y mantiene una única transacción
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ElPerritoContext _context;
        private IDbContextTransaction? _transaction;

        // Repositorios lazy-loaded
        private IProductoRepository? _productos;
        private IUsuarioRepository? _usuarios;
        private IClienteRepository? _clientes;
        private ICarritoRepository? _carritos;
        private IVentaRepository? _ventas;
        private ICategoriaRepository? _categorias;

        private bool _disposed = false;

        public UnitOfWork(ElPerritoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IProductoRepository Productos
        {
            get
            {
                _productos ??= new ProductoRepository(_context);
                return _productos;
            }
        }

        public IUsuarioRepository Usuarios
        {
            get
            {
                _usuarios ??= new UsuarioRepository(_context);
                return _usuarios;
            }
        }

        public IClienteRepository Clientes
        {
            get
            {
                _clientes ??= new ClienteRepository(_context);
                return _clientes;
            }
        }

        public ICarritoRepository Carritos
        {
            get
            {
                _carritos ??= new CarritoRepository(_context);
                return _carritos;
            }
        }

        public IVentaRepository Ventas
        {
            get
            {
                _ventas ??= new VentaRepository(_context);
                return _ventas;
            }
        }

        public ICategoriaRepository Categorias
        {
            get
            {
                _categorias ??= new CategoriaRepository(_context);
                return _categorias;
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Aquí se podría agregar logging
                throw new Exception("Error al guardar cambios en la base de datos", ex);
            }
        }

        public async Task BeginTransactionAsync()
        {
            if (_transaction != null)
            {
                throw new InvalidOperationException("Ya existe una transacción activa");
            }

            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("No hay una transacción activa para confirmar");
            }

            try
            {
                await _context.SaveChangesAsync();
                await _transaction.CommitAsync();
            }
            catch
            {
                await RollbackTransactionAsync();
                throw;
            }
            finally
            {
                if (_transaction != null)
                {
                    await _transaction.DisposeAsync();
                    _transaction = null;
                }
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("No hay una transacción activa para revertir");
            }

            try
            {
                await _transaction.RollbackAsync();
            }
            finally
            {
                if (_transaction != null)
                {
                    await _transaction.DisposeAsync();
                    _transaction = null;
                }
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Dispose de la transacción si existe
                    _transaction?.Dispose();

                    // Dispose del contexto
                    _context?.Dispose();
                }

                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
