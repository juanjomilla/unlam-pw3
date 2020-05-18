using System.Collections.Generic;
using Dao;
using Entidades;

namespace Servicios
{
    public class NecesidadServicio
    {
        private readonly INecesidadDao _necesidadDao;

        public NecesidadServicio(INecesidadDao necesidadDao)
        {
            _necesidadDao = necesidadDao;
        }

        public IEnumerable<Necesidad> GetNecesidades()
        {
            return _necesidadDao.GetNecesidades();
        }
    }
}
