using System.Collections.Generic;
using Repositorio;
using Repositorio.Repositorios;

namespace Servicios
{
    public class ServicioNecesidad
    {
        private readonly INecesidadesRepositorio _necesidadesRepository;

        public ServicioNecesidad(INecesidadesRepositorio necesidadDao)
        {
            _necesidadesRepository = necesidadDao;
        }

        public IEnumerable<Necesidades> GetNecesidadesOtrosUsuarios(int idUsuario)
        {
            return _necesidadesRepository.Get(x => x.IdUsuarioCreador != idUsuario);
        }

        public IEnumerable<Necesidades> GetNecesidadesUsuario(int idUsuario)
        {
            return _necesidadesRepository.Get(x => x.IdUsuarioCreador == idUsuario);
        }

        public IEnumerable<Necesidades> GetNecesidadesMasValoradas(int top = 5)
        {
            return _necesidadesRepository.GetNecesidadesMasValoradas(top: top);
        }

        public Necesidades GetNecesidad(int idNecesidad)
        {
            return _necesidadesRepository.Get(idNecesidad);
        }
    }
}
