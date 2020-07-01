using System.Collections.Generic;

namespace Repositorio.Repositorios
{
    public interface IDonacionesInsumosRepositorio : IRepositorio<DonacionesInsumos>
    {
        int GetTotalDonaciones(int idNecesidadDonacionInsumo);
        void CrearDonacionInsumo(List<DonacionesInsumos> nuevaDonacionInsumo);
    }
}
