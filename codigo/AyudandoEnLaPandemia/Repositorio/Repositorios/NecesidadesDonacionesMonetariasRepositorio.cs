using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio.Repositorios
{
    public class NecesidadesDonacionesMonetariasRepositorio : Repositorio <NecesidadesDonacionesMonetarias>, INecesidadesDonacionesMonetariasRepositorio
    {
        public NecesidadesDonacionesMonetariasRepositorio(Contexto contexto) : base(contexto) { }

        public NecesidadesDonacionesMonetarias BuscarNecesidad(int idNecesidad)
        {
            var necesidadDonacionMonetaria = Get(x => x.IdNecesidad == idNecesidad).FirstOrDefault();

            return necesidadDonacionMonetaria;
        }
    }
}
