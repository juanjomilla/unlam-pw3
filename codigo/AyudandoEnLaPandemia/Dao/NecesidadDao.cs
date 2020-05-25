using System.Collections.Generic;
using System.Linq;
using Repositorio;

namespace Dao
{
    public class NecesidadDao : INecesidadDao
    {
        private readonly Contexto _dbContext;

        public NecesidadDao()
        {
            _dbContext = new Contexto();
        }

        public IEnumerable<Necesidades> GetNecesidades(int top)
        {
            var query = _dbContext.Necesidades.AsQueryable();

            if (top >= 1)
            {
                query = query.Take(top);
            }

            return query.ToList();
        }
    }
}
