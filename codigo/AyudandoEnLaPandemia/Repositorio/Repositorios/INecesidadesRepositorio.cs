using System.Collections.Generic;

namespace Repositorio.Repositorios
{
    public interface INecesidadesRepositorio : IRepositorio<Necesidades>
    {
        IEnumerable<Necesidades> GetNecesidadesMasValoradas(int top);
    }
}
