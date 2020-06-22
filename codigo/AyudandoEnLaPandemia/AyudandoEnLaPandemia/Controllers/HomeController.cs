using System.Web.Mvc;
using AyudandoEnLaPandemia.ViewModels;
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
        public ActionResult Index()
        {
            var necesidades = _servicioNecesidad.GetNecesidadesMasValoradas();

            var viewModel = new HomeViewModel
            {
                TituloPagina = "Ayudando en la pandemia",
                TopNecesidades = necesidades
            };

            return View("~/Views/Home/home.cshtml", viewModel);
        }

        public ActionResult MiPerfil()
        {
            return View();
        }
    }
}