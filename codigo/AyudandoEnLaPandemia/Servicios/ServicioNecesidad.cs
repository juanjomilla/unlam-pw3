using System.Collections.Generic;
using Repositorio;
using Repositorio.Repositories;

namespace Servicios
{
    public class ServicioNecesidad
    {
        private readonly INecesidadesRepository _necesidadesRepository;

        public ServicioNecesidad(INecesidadesRepository necesidadDao)
        {
            _necesidadesRepository = necesidadDao;
        }

        public IEnumerable<Necesidades> GetNecesidades()
        {
            return _necesidadesRepository.Get();
        }
    }
}
