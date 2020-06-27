using System.Web.Mvc;
using AyudandoEnLaPandemia.ViewModels.Denuncias;
using Servicios;

namespace AyudandoEnLaPandemia.Controllers
{
    public class DenunciasController : Controller
    {
        private readonly ServicioLogin _servicioLogin;
        private readonly ServicioDenuncias _servicioDenuncias;

        public DenunciasController(ServicioLogin servicioLogin, ServicioDenuncias servicioDenuncias)
        {
            _servicioLogin = servicioLogin;
            _servicioDenuncias = servicioDenuncias;
        }

        public ActionResult AceptarDenuncia(int id)
        {
            var idUsuario = (int)Session["UsuarioID"];

            if (!_servicioLogin.EsAdministrador(idUsuario))
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.Forbidden);
            }

            _servicioDenuncias.AceptarDenuncia(id);

            return RedirectToAction("GestionDenuncias");
        }

        public ActionResult DesestimarDenuncia(int id, string mensaje = "")
        {
            var idUsuario = (int)Session["UsuarioID"];

            if (!_servicioLogin.EsAdministrador(idUsuario))
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.Forbidden);
            }

            _servicioDenuncias.DesestimarDenuncia(id);

            return RedirectToAction("GestionDenuncias");
        }

        public ActionResult GestionDenuncias()
        {
            var idUsuario = (int)Session["UsuarioID"];

            if (!_servicioLogin.EsAdministrador(idUsuario))
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.Forbidden);
            }

            var viewModel = new GestionDenunciasViewModel
            {
                Denuncias = _servicioDenuncias.ObtenerDenunciasActivas()
            };

            return View("~/Views/Denuncias/GestionDenuncias.cshtml", viewModel);
        }
    }
}