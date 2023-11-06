﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public abstract class RepositoryBase<T>: IRepositoryBase<T> where  T: class
    {
        protected RepositoryContext RepositoryContext;
        public RepositoryBase(RepositoryContext repositoryContext)=>RepositoryContext = repositoryContext;

        public IQueryable<T> GetAll(bool trackChanges) => !trackChanges ?
            RepositoryContext.Set<T>().AsNoTracking() :
            RepositoryContext.Set<T>();
        public IQueryable<T> Get(Expression<Func<T, bool>> expression, bool trackChanges) => !trackChanges ?
            RepositoryContext.Set<T>()
            .Where(expression)
            .AsNoTracking() :
            RepositoryContext.Set<T>()
            .Where(expression);
        public void Create(T entity)=>RepositoryContext.Set<T>().Add(entity);
        public void Update(T entity) => RepositoryContext.Set<T>().Update(entity);
        public void Delete(T entity) => RepositoryContext.Set<T>().Remove(entity);
    }
}
