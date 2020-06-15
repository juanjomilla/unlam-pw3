using System.Web.Mvc;
using AyudandoEnLaPandemia.ViewModels;
using Servicios;

namespace AyudandoEnLaPandemia.Controllers
{
    public class NecesidadController : Controller
    {
        private ServicioNecesidad _servicioNecesidad;

        public NecesidadController(ServicioNecesidad servicioNecesidad)
        {
            _servicioNecesidad = servicioNecesidad;
        }

        public ActionResult Detalle(int id)
        {
            var necesidad = _servicioNecesidad.GetNecesidad(id);

            if (necesidad == null)
            {
                return HttpNotFound();
            }

            var viewModel = new DetalleNecesidadViewModel
            {
                Necesidad = necesidad,
                EsPropietario = necesidad.IdUsuarioCreador == (int)Session["UsuarioID"],
                TituloPagina = necesidad.Nombre + " - Detalle de necesidad"
            };

            return View("~/Views/Necesidad/DetalleNecesidad.cshtml", viewModel);
        }

        public ActionResult ValorarNecesidad(int id, bool voto)
        {
            // primero busco si el usuario ya valoró la necesidad

            return View();
        }
    }
}