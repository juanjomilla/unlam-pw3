using System.Collections.Generic;
using Entidades;

namespace Dao
{
    public interface INecesidadDao
    {
        IEnumerable<Necesidad> GetNecesidades();
    }
}
