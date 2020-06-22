using System.Linq;

namespace Repositorio.Repositorios
{
    public class DonacionesMonetariasRepositorio : Repositorio<DonacionesMonetarias>, IDonacionesMonetariasRepositorio
    {
        public DonacionesMonetariasRepositorio(Contexto contexto) : base(contexto) { }

        public decimal GetTotalDonaciones(int idNecesidadDonacionMonetaria)
        {
            IQueryable<DonacionesMonetarias> query = _dbSet;

            return query.Where(x => x.IdNecesidadDonacionMonetaria == idNecesidadDonacionMonetaria).Sum(x => x.Dinero);
        }
    }
}
