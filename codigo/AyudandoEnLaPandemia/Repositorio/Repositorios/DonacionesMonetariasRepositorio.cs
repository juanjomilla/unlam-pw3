using System.Linq;

namespace Repositorio.Repositorios
{
    public class DonacionesMonetariasRepositorio : Repositorio<DonacionesMonetarias>, IDonacionesMonetariasRepositorio
    {
        public DonacionesMonetariasRepositorio(Contexto contexto) : base(contexto) { }

        public void CrearDonacionMonetaria(DonacionesMonetarias donacion)
        {
            using (var unitOfWork = new UnitOfWork(_dbContext))
            {
                unitOfWork.DonacionesMonetarias.Add(donacion);
                unitOfWork.SaveChanges();
            }
        }

        public decimal GetTotalDonaciones(int idNecesidadDonacionMonetaria)
        {
            IQueryable<DonacionesMonetarias> query = _dbSet;

            return query.Where(x => x.IdNecesidadDonacionMonetaria == idNecesidadDonacionMonetaria).Sum(x => x.Dinero);
        }
    }
}
