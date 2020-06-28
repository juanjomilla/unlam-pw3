using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Repositorio.Repositorios
{
    public class NecesidadesDonacionesInsumoRepositorio : Repositorio<NecesidadesDonacionesInsumos>, INecesidadesDonacionesInsumoRepositorio
    {
        public NecesidadesDonacionesInsumoRepositorio(Contexto contexto) : base(contexto) { }

        public IEnumerable<NecesidadesDonacionesInsumos> BuscarNecesidad(int idNecesidad)
        {
            IQueryable<NecesidadesDonacionesInsumos> query = _dbSet;

            var necesidadDonacionInsumos= query.Where(x => x.IdNecesidad == idNecesidad).ToList();

            return necesidadDonacionInsumos;
        }
    }
}