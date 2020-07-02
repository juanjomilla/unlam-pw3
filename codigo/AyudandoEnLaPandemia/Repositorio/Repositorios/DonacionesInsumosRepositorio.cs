using System.Collections.Generic;
using System.Linq;

namespace Repositorio.Repositorios
{
    public class DonacionesInsumosRepositorio : Repositorio<DonacionesInsumos>, IDonacionesInsumosRepositorio
    {
        public DonacionesInsumosRepositorio(Contexto contexto) : base(contexto) { }

        public void CrearDonacionInsumo(List<DonacionesInsumos> nuevaDonacionInsumo)
        {
            using (var unitOfWork = new UnitOfWork(_dbContext))
            {
                foreach (var insumo in nuevaDonacionInsumo)
                {
                  unitOfWork.DonacionesInsumos.Add(insumo);
                }

                unitOfWork.SaveChanges();
            }            
        }

        public int GetTotalDonaciones(int idNecesidadDonacionInsumo)
        {
            IQueryable<DonacionesInsumos> query = _dbSet;
            var cant = query.Where(x => x.IdNecesidadDonacionInsumo == idNecesidadDonacionInsumo).Count();

            if (cant == 0)
            {
                return 0;
            }

            return query.Where(x => x.IdNecesidadDonacionInsumo == idNecesidadDonacionInsumo).Sum(x => x.Cantidad);
        
         }
    }
}
