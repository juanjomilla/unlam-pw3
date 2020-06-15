using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Repositorio
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly Contexto _dbContext;
        private readonly DbSet<T> _dbSet;

        public Repository(Contexto dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public IEnumerable<T> Get(
            Expression<Func<T, bool>> filtro = null,
            int top = 0)
        {
            IQueryable<T> query = _dbSet;

            if (top > 0)
            {
                query = query.Take(top);
            }

            if (filtro != null)
            {
                query = query.Where(filtro);
            }

            return query.ToList();
        }
    }
}
