using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using RM_API.Repositories.Interfaces;
using System.Linq.Expressions;
using RM_API.Models;

namespace RM_API.Repositories.Implementations
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected RM_APIContext RepositoryContext { get; set; }
        public RepositoryBase(RM_APIContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }
        public void Create(T entity)
        {
            RepositoryContext.Set<T>()
                .Add(entity);
        }

        public void Delete(T entity)
        {
            RepositoryContext.Set<T>()
                .Remove(entity);
        }

        public IQueryable<T> FindAll()
        {
            return RepositoryContext.Set<T>()
                .AsNoTrackingWithIdentityResolution();
        }

        public IQueryable<T> FindAll(Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null)
        {
            IQueryable<T> queryable = RepositoryContext.Set<T>();

            if (includes != null)
            {
                queryable = includes(queryable);
            }
            return queryable.AsNoTrackingWithIdentityResolution();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return RepositoryContext.Set<T>()
                .Where(expression)
                .AsNoTrackingWithIdentityResolution();
        }

        public void SaveChanges()
        {
            RepositoryContext.SaveChanges();
        }

        public void Update(T entity)
        {
            RepositoryContext.Set<T>()
                .Update(entity);
        }
    }
}
