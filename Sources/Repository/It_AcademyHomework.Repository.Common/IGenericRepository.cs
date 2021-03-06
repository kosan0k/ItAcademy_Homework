﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using It_AcademyHomework.Repository.Common.Models;

namespace It_AcademyHomework.Repository.Common
{
    public interface IGenericRepository<T> : IDisposable where T : IEntity
    {
        Task<IEnumerable<T>> GetAsync(
            Expression<Func<T, bool>> queryFilter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string[] includeProperties = null
        );

        Task<RepositoryOperationResult> AddAsync(T itemToAdd);

        Task<RepositoryOperationResult> DeleteAsync(T itemToDelete);

        Task<RepositoryOperationResult> UpdateAsync(T itemToUpdate);
    }
}
