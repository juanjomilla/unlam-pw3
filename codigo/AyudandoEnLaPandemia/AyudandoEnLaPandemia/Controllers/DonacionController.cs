using AyudandoEnLaPandemia.ViewModels;
using AyudandoEnLaPandemia.ViewModels.Donaciones;
using Repositorio;
using Servicios;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

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
                var necesidadesDonacionesInsumos = _servicioDonaciones.GetNecesidadesDonacionesInsumos(idNecesidad);
                List<DonacionesInsumosViewModel> listDonacionesInsumos = new List<DonacionesInsumosViewModel>();

              //  DonacionesInsumosListViewModel listDonacionesInsumosViewModel = new DonacionesInsumosListViewModel();

                foreach (NecesidadesDonacionesInsumos n in necesidadesDonacionesInsumos)
                {
                    DonacionesInsumosViewModel donacionesInsumosViewModel = new DonacionesInsumosViewModel();

                    int totalDonaciones = _servicioDonaciones.GetTotalDonacionesInsumo(n.IdNecesidadDonacionInsumo);
                    bool statusCompleto = _servicioDonaciones.ValidarDonacionCompleta(totalDonaciones, n.Cantidad);
                    if (!statusCompleto)
                    {
                        donacionesInsumosViewModel.CantidadRestante = n.Cantidad - totalDonaciones;
                    }
                    else
                    {
                        donacionesInsumosViewModel.CantidadRestante = 0;
                    }
                    donacionesInsumosViewModel.Cantidad = n.Cantidad;
                    donacionesInsumosViewModel.CantidadTotal = totalDonaciones;
                    donacionesInsumosViewModel.statusCompleto = statusCompleto;
                    donacionesInsumosViewModel.Nombre = n.Nombre;
                    donacionesInsumosViewModel.IdNecesidadDonacionInsumo = n.IdNecesidadDonacionInsumo;
                    listDonacionesInsumos.Add(donacionesInsumosViewModel);       

                }

                DonacionesInsumosListViewModel nuevo = new DonacionesInsumosListViewModel();
                nuevo.InsumosList = listDonacionesInsumos;

                return View("~/Views/Donacion/DonacionInsumos.cshtml", nuevo);
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

        [HttpGet]
        public ActionResult DonacionInsumos(String mensaje = "")
        {
            ViewBag.Message = mensaje;
            return View();
        }

        [HttpPost]
        public ActionResult DonacionInsumos(DonacionesInsumosListViewModel listDonacionesInsumos)
        {
            bool cantidadCero = ValidarCantidadesCero(listDonacionesInsumos.InsumosList);

            if (cantidadCero == true) {

                ViewBag.Message = "Error";
                return View(listDonacionesInsumos);

            }

            List<DonacionesInsumos> nuevaDonacionInsumolist = new List<DonacionesInsumos>();

            foreach (var insumo in listDonacionesInsumos.InsumosList)
            {

                if (insumo.CantidadAdonar != 0) {

                    DonacionesInsumos nuevaDonacionInsumo = new DonacionesInsumos();
                    nuevaDonacionInsumo.IdNecesidadDonacionInsumo = insumo.IdNecesidadDonacionInsumo;
                    nuevaDonacionInsumo.Cantidad = insumo.CantidadAdonar;
                    nuevaDonacionInsumo.IdUsuario = (int)Session["UsuarioID"];

                    nuevaDonacionInsumolist.Add(nuevaDonacionInsumo);

                }
            }

            _servicioDonaciones.CrearDonacionInsumo(nuevaDonacionInsumolist);

            return RedirectToAction("Index", "Home");

        }

        private bool ValidarCantidadesCero(List<DonacionesInsumosViewModel> insumosList)
        {
            bool cantidadesCero =true;

            foreach (var insumo in insumosList)
            {
                if (insumo.CantidadAdonar != 0)
                {
                    cantidadesCero = false;
                }
            }

            return cantidadesCero;
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