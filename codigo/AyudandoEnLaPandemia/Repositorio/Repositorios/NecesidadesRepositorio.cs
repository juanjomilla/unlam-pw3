using System.Collections.Generic;
using System.Linq;

namespace Repositorio.Repositorios
{
    public class NecesidadesRepositorio : Repositorio<Necesidades>, INecesidadesRepositorio
    {
        public NecesidadesRepositorio(Contexto dbContext) : base(dbContext) { }

        public IEnumerable<Necesidades> GetNecesidadesMasValoradas(int top)
        {
            IQueryable<Necesidades> query = _dbSet;

            return query
                .Where(x => x.Estado != 3)
                .OrderByDescending(x => x.Valoracion)
                .Take(top);
        }
    }
}
