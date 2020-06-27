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

        public DonacionController (ServicioDonaciones servicioDonaciones)
        {
            _servicioDonaciones = servicioDonaciones;
        }

        public ActionResult Donacion(string tipoDonacion, int idNecesidad)
        {
            if (tipoDonacion.Equals("monetario"))
            {
                
                return RedirectToAction("DonacionMonetaria", new { idNecesidad });
            }
            else
            {
                return RedirectToAction("DonacionInsumos");
            }
            
        }
        
        [HttpGet]
        public ActionResult DonacionMonetaria( int idNecesidad )
        {
            ViewBag.idNecesidad = idNecesidad;
            return View();
        }
        [HttpPost]
        public ActionResult DonacionMonetaria(DonacionesMonetarias donacion, int idNecesidad)
        {
            if (!ModelState.IsValid)
            {
                return View(donacion);
            }

           // int IdNecesidadDonacionMonetaria = _servicioDonaciones.BuscarIdNecesidadDonacionMonetaria(idNecesidad);
            _servicioDonaciones.CrearDonacionMonetaria(donacion);
            return View();
        }

        public ActionResult DonacionInsumos()
        {
            return View();
        }
    }
}