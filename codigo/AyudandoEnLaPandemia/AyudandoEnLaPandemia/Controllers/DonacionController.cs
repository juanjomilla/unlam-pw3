using AyudandoEnLaPandemia.ViewModels;
using Repositorio;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AyudandoEnLaPandemia.Controllers
{
    public class DonacionController : Controller
    {
        private ServicioDonaciones _servicioDonaciones;
        private ServicioNecesidad _servicioNecesidad;

        public DonacionController (ServicioDonaciones servicioDonaciones, ServicioNecesidad servicioNecesidad)
        {
            _servicioDonaciones = servicioDonaciones;
            _servicioNecesidad = servicioNecesidad;
        }

        public ActionResult Donacion(string TipoDonacion, int idNecesidad)
        {
            var necesidadesDonacionesMonetarias = _servicioDonaciones.GetNecesidadesDonacionesMonetarias(idNecesidad);

            TempData["IdnecesidadesDonacionesMonetarias"] = necesidadesDonacionesMonetarias.IdNecesidadDonacionMonetaria;
            TempData["Dinero"] = necesidadesDonacionesMonetarias.Dinero;
            TempData["CBU"] = necesidadesDonacionesMonetarias.CBU;

            if (TipoDonacion.Equals("monetario"))
            {
                return RedirectToAction("DonacionMonetaria");
            }
            else
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

            var idUsuario = (int)Session["UsuarioID"]; 
            nuevaDonacionMoentaria.IdUsuario = idUsuario;
            nuevaDonacionMoentaria.FechaCreacion = DateTime.Today;
            nuevaDonacionMoentaria.ArchivoTransferencia = _servicioDonaciones.GuardarAdjunto(nuevaDonacionMoentaria.IdUsuario, archivo);

            _servicioDonaciones.CrearDonacionMonetaria(nuevaDonacionMoentaria);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult DonacionInsumos()
        {
            return View();
        }
    }
}