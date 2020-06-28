using System;
using System.Web;
using System.Web.Mvc;
using AyudandoEnLaPandemia.ViewModels.Donaciones;
using Repositorio;
using Servicios;

namespace AyudandoEnLaPandemia.Controllers
{
    public class DonacionController : Controller
    {
        private readonly ServicioDonaciones _servicioDonaciones;
        private readonly ServicioNecesidad _servicioNecesidad;

        public DonacionController(ServicioDonaciones servicioDonaciones, ServicioNecesidad servicioNecesidad)
        {
            _servicioDonaciones = servicioDonaciones;
            _servicioNecesidad = servicioNecesidad;
        }

        public ActionResult Donacion(int idNecesidad)
        {
            int TipoDonacion = _servicioNecesidad.GetTipoNecesidad(idNecesidad);

            if (TipoDonacion == 0) // 0 Monetario
            {
                var necesidadesDonacionesMonetarias = _servicioDonaciones.GetNecesidadesDonacionesMonetarias(idNecesidad);
                decimal totalDonaciones = _servicioDonaciones.GetTotalDonacionesMonetaria(necesidadesDonacionesMonetarias.IdNecesidadDonacionMonetaria);
                decimal totalRestante = necesidadesDonacionesMonetarias.Dinero - totalDonaciones;
                TempData["IdnecesidadesDonacionesMonetarias"] = necesidadesDonacionesMonetarias.IdNecesidadDonacionMonetaria;
                TempData["Dinero"] = necesidadesDonacionesMonetarias.Dinero;
                TempData["DineroRestante"] = totalRestante;
                TempData["CBU"] = necesidadesDonacionesMonetarias.CBU;
                return RedirectToAction("DonacionMonetaria");
            }
            else // 1 Insumos
            {
                return RedirectToAction("DonacionInsumos");
            }
        }
        
        [HttpGet]
        public ActionResult DonacionMonetaria()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DonacionMonetaria(DonacionesMonetarias nuevaDonacionMoentaria, HttpPostedFileBase archivo)
        {
            if (!ModelState.IsValid || archivo == null)
            {
                return View(nuevaDonacionMoentaria);
            }

            nuevaDonacionMoentaria.IdUsuario = (int)Session["UsuarioID"];
            nuevaDonacionMoentaria.FechaCreacion = DateTime.Today;
            nuevaDonacionMoentaria.ArchivoTransferencia = _servicioDonaciones.GuardarAdjunto(nuevaDonacionMoentaria.IdUsuario, archivo);

            _servicioDonaciones.CrearDonacionMonetaria(nuevaDonacionMoentaria);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult DonacionInsumos()
        {
            return View();
        }

        public ActionResult HistorialDonaciones() 
        {
            var idUsuario = (int)Session["UsuarioID"];

            var result = _servicioDonaciones.GetHistorialDonaciones(idUsuario);

            var viewModel = new HistorialDonacionesViewModel
            {
                ListaDonaciones = result
            };

            return View("~/Views/Donacion/HistorialDonaciones.cshtml", viewModel);
        }
    }
}