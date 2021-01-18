using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using It_AcademyHomework.Repository.Common;
using It_AcademyHomework.Repository.Common.Models;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace It_AcademyHomework.Repository.EntityFramework
{
    public class EfGenericRepository<T> : IGenericRepository<T> where T : class,IEntity
    {
        private CommonRepositoryContext _context;
        private DbSet<T> _dbSet;
        private static ILogger _logger = NLog.LogManager.GetCurrentClassLogger();

        public EfGenericRepository(CommonRepositoryContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> queryFilter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string[] includeProperties = null)
        {
            IQueryable<T> query = _dbSet;

            if (queryFilter != null)
            {
                query = query.Where(queryFilter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }

        }
        

        public async Task<RepositoryOperationResult> AddAsync(T itemToAdd)
        {
            RepositoryOperationResult result;
            try
            {
                await _dbSet.AddAsync(itemToAdd);
                await _context.SaveChangesAsync();
                result = new RepositoryOperationResult(true);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"An error occured when inserting {nameof(itemToAdd)} entity");
                result = new RepositoryOperationResult(false, new List<string>() {ex.Message});
            }
            return result;

        }

        public async Task<RepositoryOperationResult> DeleteAsync(T itemToDelete)
        {
            RepositoryOperationResult result;
            try
            {
                _dbSet.Remove(itemToDelete);
                await _context.SaveChangesAsync();
                result = new RepositoryOperationResult(true);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"An error occured when deleting {nameof(itemToDelete)} entity");
                result = new RepositoryOperationResult(false, new List<string>() { ex.Message });
            }
            return result;
        }

        public async Task<RepositoryOperationResult> UpdateAsync(T itemToUpdate)
        {
            RepositoryOperationResult result;
            try
            {
                var state = GetState(itemToUpdate);

                if (state == EntityState.Detached)
                {
                    var existingAttachedEntity = _dbSet.Local.FirstOrDefault(e => e.Id == itemToUpdate.Id);

                    if (existingAttachedEntity != null)
                    {
                        var attachedEntry = _context.Entry(existingAttachedEntity);
                        attachedEntry.CurrentValues.SetValues(itemToUpdate);
                    }
                }
                else
                {
                    _dbSet.Attach(itemToUpdate);
                    SetEntityStateModified(itemToUpdate);
                }

                await _context.SaveChangesAsync();
                result = new RepositoryOperationResult(true);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"An error occured when updating {nameof(itemToUpdate)} entity");
                result = new RepositoryOperationResult(false, new List<string>() { ex.Message });
            }
            return result;
        }

        internal virtual EntityState GetState(T entity)
        {
            return _context.Entry(entity).State;
        }

        internal virtual void SetEntityStateModified(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }



        #region IDisposable Support
        private bool disposedValue = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    DisposeContext();
                    _context = null;
                    _dbSet = null;
                }

                disposedValue = true;
            }
        }

        internal virtual void DisposeContext()
        {
            _context.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion

    }
}
