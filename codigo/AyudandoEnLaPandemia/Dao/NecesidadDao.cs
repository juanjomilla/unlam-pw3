using System.Collections.Generic;
using Entidades;

namespace Dao
{
    public class NecesidadDao : INecesidadDao
    {
        private IEnumerable<Necesidad> _necesidades;

        public NecesidadDao()
        {
            _necesidades = GenerarNecesidades();
        }

        public IEnumerable<Necesidad> GetNecesidades()
        {
            return _necesidades;
        }

        private IEnumerable<Necesidad> GenerarNecesidades()
        {
            return new List<Necesidad>
            {
                new Necesidad { Nombre = "Aceite de cocina" },
                new Necesidad { Nombre = "Alcohol en gel" }
            };
        }
    }
}
