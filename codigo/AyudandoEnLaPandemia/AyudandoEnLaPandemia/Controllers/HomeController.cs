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
            var necesidades = _servicioNecesidad.GetNecesidades();

            var viewModel = new IndexViewModel
            {
                TituloPagina = "Ayudando en la pandemia",
                TopNecesidades = necesidades
            };

            return View("~/Views/Home/index.cshtml", viewModel);
        }

        [HttpGet]
        public ActionResult Home()
        {
            var viewModel = new HomeViewModel()
            {
                TituloPagina = "Home",
                MisNecesidades = _servicioNecesidad.GetNecesidades(),
                Necesidades = _servicioNecesidad.GetNecesidades()
            };

            return View("~/Views/Home/home.cshtml", viewModel);
        }

        public ActionResult MiPerfil()
        {
            return View();
        }
    }
}