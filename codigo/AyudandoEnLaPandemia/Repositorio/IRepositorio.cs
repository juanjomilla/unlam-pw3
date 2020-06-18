using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Repositorio
{
    public interface IRepositorio<T> where T : class
    {
        IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            int top = 0);

        void Add(T entidad);
    }
}
