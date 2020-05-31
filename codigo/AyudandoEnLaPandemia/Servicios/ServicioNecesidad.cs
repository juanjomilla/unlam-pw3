using System.Collections.Generic;
using Repositorio;

namespace Servicios
{
    public class ServicioNecesidad
    {
        private readonly IRepository<Necesidades> _necesidadesRepository;

        public ServicioNecesidad(IRepository<Necesidades> necesidadDao)
        {
            _necesidadesRepository = necesidadDao;
        }

        public IEnumerable<Necesidades> GetNecesidades()
        {
            return _necesidadesRepository.Get();
        }
    }
}
