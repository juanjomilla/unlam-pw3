using System.Collections.Generic;
using System.Linq;

namespace Repositorio.Repositorios
{
    public class NecesidadesRepositorio : Repository<Necesidades>, INecesidadesRepositorio
    {
        public NecesidadesRepositorio(Contexto dbContext) : base(dbContext) { }

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
