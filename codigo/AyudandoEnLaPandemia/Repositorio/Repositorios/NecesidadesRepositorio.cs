using System.Collections.Generic;
using System.Linq;

namespace Repositorio.Repositorios
{
    public class NecesidadesRepositorio : Repository<Necesidades>, INecesidadesRepositorio
    {
        public NecesidadesRepositorio(Contexto dbContext) : base(dbContext) { }

        public IEnumerable<Necesidades> GetNecesidadesMasValoradas(int top = 5)
        {
            IQueryable<Necesidades> query = _dbSet;

            return query
                .OrderByDescending(x => x.Valoracion)
                .Take(top);
        }
    }
}
