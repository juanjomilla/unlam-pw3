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
            // TODO: dejar este return cuando se tenga bootstrap en el site
            return new List<Necesidad>
            {
                new Necesidad { Nombre = "Aceite de cocina", UrlImagen = "https://image.freepik.com/foto-gratis/aceite-cocina-botella-plastico-blanco_35712-553.jpg" },
                new Necesidad { Nombre = "Alcohol en gel", UrlImagen = "http://d26lpennugtm8s.cloudfront.net/stores/895/251/products/gel1litrokomili11-fd2c5b8eb723ade6c015871300229099-240-0.jpg" }
            };

            //return new List<Necesidad>
            //{
            //    new Necesidad { Nombre = "Aceite de cocina" },
            //    new Necesidad { Nombre = "Alcohol en gel" },
            //    new Necesidad { Nombre = "Alimentos no perecederos" }
            //};
        }
    }
}
