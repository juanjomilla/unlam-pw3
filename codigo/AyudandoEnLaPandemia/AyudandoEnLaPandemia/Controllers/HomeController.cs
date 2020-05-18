using System.Web.Mvc;
using System.Web.Script.Serialization;
using Servicios;

namespace AyudandoEnLaPandemia.Controllers
{
    public class HomeController : Controller
    {
        private readonly ServicioNecesidad _servicioNecesidad;

        public HomeController(ServicioNecesidad servicioNecesidad)
        {
            _servicioNecesidad = servicioNecesidad;
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
            var necesidades = _servicioNecesidad.GetNecesidades();

            return serializer.Serialize(necesidades);
        }
    }
}