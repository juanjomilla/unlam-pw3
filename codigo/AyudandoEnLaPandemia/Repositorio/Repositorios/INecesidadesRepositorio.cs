using System.Collections.Generic;

namespace Repositorio.Repositorios
{
    public interface INecesidadesRepositorio : IRepository<Necesidades>
    {
        IEnumerable<Necesidades> GetNecesidadesMasValoradas(int top);
    }
}
