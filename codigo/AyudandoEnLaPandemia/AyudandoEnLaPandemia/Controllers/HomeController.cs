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
                MisNecesidades = _servicioNecesidad.GetNecesidadesUsuario((int)Session["UsuarioID"]),
                Necesidades = _servicioNecesidad.GetNecesidadesOtrosUsuarios((int)Session["UsuarioID"])
            };

            return View("~/Views/Home/home.cshtml", viewModel);
        }

            public ActionResult AcercaDe()
            {
            return View("~/Views/Home/acercaDe.cshtml");
            }
    }
}