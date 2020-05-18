using System.Collections.Generic;
using Dao;
using Entidades;

namespace Servicios
{
    public class ServicioNecesidad
    {
        private readonly INecesidadDao _necesidadDao;

        public ServicioNecesidad(INecesidadDao necesidadDao)
        {
            _necesidadDao = necesidadDao;
        }

        public IEnumerable<Necesidad> GetNecesidades()
        {
            return _necesidadDao.GetNecesidades();
        }
    }
}
