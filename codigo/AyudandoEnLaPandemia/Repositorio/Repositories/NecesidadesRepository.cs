using System.Collections.Generic;
using System.Linq;

namespace Repositorio.Repositories
{
    public class NecesidadesRepository : Repository<Necesidades>, INecesidadesRepository
    {
        public NecesidadesRepository(Contexto dbContext) : base(dbContext) { }

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
