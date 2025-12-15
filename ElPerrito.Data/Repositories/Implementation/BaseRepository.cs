using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ElPerrito.Data.Context;
using ElPerrito.Data.Repositories.Interfaces;

namespace ElPerrito.Data.Repositories.Implementation
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly ElPerritoContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        protected BaseRepository(ElPerritoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = context.Set<TEntity>();
        }

        #region Template Method Hooks

        protected virtual Task BeforeAddAsync(TEntity entity)
        {
            return Task.CompletedTask;
        }

        protected virtual Task AfterAddAsync(TEntity entity)
        {
            return Task.CompletedTask;
        }

        protected virtual Task BeforeUpdateAsync(TEntity entity)
        {
            return Task.CompletedTask;
        }

        protected virtual Task AfterUpdateAsync(TEntity entity)
        {
            return Task.CompletedTask;
        }

        protected virtual Task BeforeDeleteAsync(TEntity entity)
        {
            return Task.CompletedTask;
        }

        protected virtual Task AfterDeleteAsync(TEntity entity)
        {
            return Task.CompletedTask;
        }

        protected virtual Task ValidateEntityAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            return Task.CompletedTask;
        }

        protected virtual IQueryable<TEntity> ApplyIncludes(IQueryable<TEntity> query)
        {
            return query;
        }

        #endregion

        #region Operaciones de lectura

        public virtual async Task<TEntity?> GetByIdAsync(int id)
        {
            var query = ApplyIncludes(_dbSet);
            return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, GetKeyPropertyName()) == id);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var query = ApplyIncludes(_dbSet);
            return await query.ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var query = ApplyIncludes(_dbSet);
            return await query.Where(predicate).ToListAsync();
        }

        public virtual async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var query = ApplyIncludes(_dbSet);
            return await query.FirstOrDefaultAsync(predicate);
        }

        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null)
        {
            return predicate == null
                ? await _dbSet.CountAsync()
                : await _dbSet.CountAsync(predicate);
        }

        public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        public virtual async Task<(IEnumerable<TEntity> Items, int TotalCount)> GetPagedAsync(
            int page,
            int pageSize,
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 20;

            IQueryable<TEntity> query = ApplyIncludes(_dbSet);

            if (filter != null)
            {
                query = query.Where(filter);
            }

            var totalCount = await query.CountAsync();

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }

        #endregion

        #region Operaciones de escritura (Template Method)

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            await ValidateEntityAsync(entity);
            await BeforeAddAsync(entity);

            var result = await _dbSet.AddAsync(entity);

            await AfterAddAsync(entity);

            return result.Entity;
        }

        public virtual async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            var entityList = entities.ToList();

            foreach (var entity in entityList)
            {
                await ValidateEntityAsync(entity);
                await BeforeAddAsync(entity);
            }

            await _dbSet.AddRangeAsync(entityList);

            foreach (var entity in entityList)
            {
                await AfterAddAsync(entity);
            }

            return entityList;
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            await ValidateEntityAsync(entity);
            await BeforeUpdateAsync(entity);

            _dbSet.Update(entity);

            await AfterUpdateAsync(entity);
        }

        public virtual async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            var entityList = entities.ToList();

            foreach (var entity in entityList)
            {
                await ValidateEntityAsync(entity);
                await BeforeUpdateAsync(entity);
            }

            _dbSet.UpdateRange(entityList);

            foreach (var entity in entityList)
            {
                await AfterUpdateAsync(entity);
            }
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            await ValidateEntityAsync(entity);
            await BeforeDeleteAsync(entity);

            _dbSet.Remove(entity);

            await AfterDeleteAsync(entity);
        }

        public virtual async Task DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            var entityList = entities.ToList();

            foreach (var entity in entityList)
            {
                await ValidateEntityAsync(entity);
                await BeforeDeleteAsync(entity);
            }

            _dbSet.RemoveRange(entityList);

            foreach (var entity in entityList)
            {
                await AfterDeleteAsync(entity);
            }
        }

        public virtual async Task<bool> DeleteByIdAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null)
            {
                return false;
            }

            await DeleteAsync(entity);
            return true;
        }

        #endregion

        #region Métodos auxiliares

        protected virtual string GetKeyPropertyName()
        {
            var entityType = typeof(TEntity).Name;
            var keyProperty = typeof(TEntity).GetProperty($"Id{entityType}");
            return keyProperty != null ? keyProperty.Name : "Id";
        }

        #endregion
    }
}
