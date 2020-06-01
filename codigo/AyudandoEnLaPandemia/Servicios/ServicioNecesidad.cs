using System.Collections.Generic;
using Repositorio;
using Repositorio.Repositories;

namespace Servicios
{
    public class ServicioNecesidad
    {
        private readonly INecesidadesRepositorio _necesidadesRepository;

        public ServicioNecesidad(INecesidadesRepositorio necesidadDao)
        {
            _necesidadesRepository = necesidadDao;
        }

        public IEnumerable<Necesidades> GetNecesidades()
        {
            return _necesidadesRepository.Get();
        }
    }
}
