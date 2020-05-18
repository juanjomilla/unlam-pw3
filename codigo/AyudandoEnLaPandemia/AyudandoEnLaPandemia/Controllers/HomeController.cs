using System.Web.Mvc;
using System.Web.Script.Serialization;
using Servicios;

namespace AyudandoEnLaPandemia.Controllers
{
    public class HomeController : Controller
    {
        private readonly NecesidadServicio _necesidadServicio;

        public HomeController(NecesidadServicio necesidadServicio)
        {
            _necesidadServicio = necesidadServicio;
        }

        [HttpGet]
        public string Index()
        {
            return "index";
        }

        [HttpGet]
        public string GetTopNecesidades()
        {
            var serializer = new JavaScriptSerializer();
            var necesidades = _necesidadServicio.GetNecesidades();

            return serializer.Serialize(necesidades);
        }
    }
}