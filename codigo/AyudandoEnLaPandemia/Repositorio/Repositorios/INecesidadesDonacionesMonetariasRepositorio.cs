using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio.Repositorios
{
    public interface INecesidadesDonacionesMonetariasRepositorio : IRepositorio<NecesidadesDonacionesMonetarias>
    {
        NecesidadesDonacionesMonetarias BuscarNecesidad(int idNecesidad);
    }
}
