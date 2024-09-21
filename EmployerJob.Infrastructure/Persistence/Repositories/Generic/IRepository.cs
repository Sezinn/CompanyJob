﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EmployerJob.Infrastructure.Persistence.Repositories.Generic
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Table { get; }
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        Task AddAsync(T entity);
        void Update(T entity);
        void Remove(T entity);

        Task<bool> SaveChangesAsync();
    }
}
