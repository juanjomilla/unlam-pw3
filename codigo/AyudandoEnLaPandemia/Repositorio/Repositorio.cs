using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Repositorio
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        protected readonly Contexto _dbContext;
        protected readonly DbSet<T> _dbSet;

        public Repositorio(Contexto dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            int top = 0)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.ToList();
        }

        public void Add(T entidad)
        {
            _dbSet.Add(entidad);
        }

        public T Get(int id)
        {
            return _dbSet.Find(id);
        }
    }
}
