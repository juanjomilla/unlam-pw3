using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Dao;
using Repositorio;

namespace Servicios
{
    public class ServicioNecesidad
    {
        private readonly INecesidadDao _necesidadDao;

        public ServicioNecesidad(INecesidadDao necesidadDao)
        {
            _necesidadDao = necesidadDao;
        }

        public IEnumerable<Necesidades> GetNecesidades(int top = 0)
        {
            return _necesidadDao.GetNecesidades(top);
        }

        public IEnumerable<Necesidades> GetMisNecesidades()
        {
            return _necesidadDao.GetNecesidades(0);
        }

        public IEnumerable<Necesidades> GetOtrasNecesidades()
        {
            return _necesidadDao.GetNecesidades(0);
        }
    }
}
