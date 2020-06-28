using System.Collections.Generic;

namespace Repositorio.Repositorios
{
    public interface INecesidadesDonacionesInsumoRepositorio : IRepositorio<NecesidadesDonacionesInsumos>
    {
        IEnumerable<NecesidadesDonacionesInsumos> BuscarNecesidad(int idNecesidad);
    }
}