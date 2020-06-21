using System.Linq;

namespace Repositorio.Repositorios
{
    public class DonacionesInsumosRepositorio : Repositorio<DonacionesInsumos>, IDonacionesInsumosRepositorio
    {
        public DonacionesInsumosRepositorio(Contexto contexto) : base(contexto) { }

        public decimal GetTotalDonaciones(int idNecesidadDonacionInsumo)
        {
            IQueryable<DonacionesInsumos> query = _dbSet;

            return query.Where(x => x.IdNecesidadDonacionInsumo == idNecesidadDonacionInsumo).Sum(x => x.Cantidad);
        }
    }
}
