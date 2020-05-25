using System.Collections.Generic;
using Repositorio;

namespace Dao
{
    public interface INecesidadDao
    {
        IEnumerable<Necesidades> GetNecesidades(int top);
    }
}
