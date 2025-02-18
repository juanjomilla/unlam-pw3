﻿using AyudandoEnLaPandemia.ViewModels;
using AyudandoEnLaPandemia.ViewModels.Donaciones;
using Repositorio;
using Servicios;
using Servicios.Models;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace AyudandoEnLaPandemia.Controllers
{
    public class DonacionController : BaseController
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

                var donacion = new DonacionMonetariaViewModel
                {
                    IdNecesidadDonacionMonetaria = necesidadesDonacionesMonetarias.IdNecesidadDonacionMonetaria,
                    Dinero = (Double)necesidadesDonacionesMonetarias.Dinero,
                    totalRestante = (Double)totalRestante,
                    CBU = necesidadesDonacionesMonetarias.CBU
                };

                return View("~/Views/Donacion/DonacionMonetaria.cshtml", donacion);
            }
            else // 1 Insumos
            {
                var necesidadesDonacionesInsumos = _servicioDonaciones.GetNecesidadesDonacionesInsumos(idNecesidad);
                List<DonacionesInsumosViewModel> listDonacionesInsumos = new List<DonacionesInsumosViewModel>();


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
        public ActionResult DonacionMonetaria(DonacionMonetariaViewModel donacionMonetaria, HttpPostedFileBase archivo)
        {
       
            if (archivo == null)
            {
                ModelState.AddModelError("ArchivoEmpty", "Se debe adjuntar archivo");
            }

            if (donacionMonetaria.DineroAdonar < 1 || donacionMonetaria.DineroAdonar > donacionMonetaria.totalRestante)
            {
                ModelState.AddModelError("CantidadDineroAdonar", "La cantidad de dinero no puede ser 0 ni mayor a la cantidad pendiente por donar");
            }

            if (!ModelState.IsValid)
            {
                return View(donacionMonetaria);
            }

            DonacionesMonetarias nuevaDonacionMonetaria = new DonacionesMonetarias();

            nuevaDonacionMonetaria.IdNecesidadDonacionMonetaria = donacionMonetaria.IdNecesidadDonacionMonetaria;
            nuevaDonacionMonetaria.Dinero = donacionMonetaria.DineroAdonar;
            nuevaDonacionMonetaria.IdUsuario = GetIdUsuario();
            nuevaDonacionMonetaria.FechaCreacion = DateTime.Today;
            nuevaDonacionMonetaria.ArchivoTransferencia = _servicioDonaciones.GuardarAdjunto(nuevaDonacionMonetaria.IdUsuario, archivo);

            _servicioDonaciones.CrearDonacionMonetaria(nuevaDonacionMonetaria);

            return RedirectToAction("DonacionConfirmada");
        }

        public ActionResult DonacionConfirmada()
        {
            return View();
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
            bool cantidadCeroOdeMas = ValidarCantidadesCeroOdeMas(listDonacionesInsumos.InsumosList);

            if (cantidadCeroOdeMas == true) {

                ViewBag.Message = "NotOK";
                return View(listDonacionesInsumos);

            }

            if (!ModelState.IsValid)
            {
                return View(listDonacionesInsumos);
            }

            List<DonacionesInsumos> nuevaDonacionInsumolist = new List<DonacionesInsumos>();

            foreach (var insumo in listDonacionesInsumos.InsumosList)
            {

                if (insumo.CantidadAdonar != 0) {

                    DonacionesInsumos nuevaDonacionInsumo = new DonacionesInsumos
                    {
                        IdNecesidadDonacionInsumo = insumo.IdNecesidadDonacionInsumo,
                        Cantidad = insumo.CantidadAdonar,
                        IdUsuario = GetIdUsuario(),
                        FechaCreacion = DateTime.Today
                    };

                    nuevaDonacionInsumolist.Add(nuevaDonacionInsumo);

                }
            }

            _servicioDonaciones.CrearDonacionInsumo(nuevaDonacionInsumolist);

            return RedirectToAction("DonacionConfirmada");

        }

        private bool ValidarCantidadesCeroOdeMas(List<DonacionesInsumosViewModel> insumosList)
        {
            bool cantidadesCero =true;

            bool cantidadesdeMas = false;

            bool cantidadesCeroOdeMas = false;

            foreach (var insumo in insumosList)
            {
                if (insumo.CantidadAdonar != 0)
                {
                    cantidadesCero = false;
                }

                if (insumo.CantidadAdonar > insumo.CantidadRestante)
                {
                    cantidadesdeMas = true;
                }
            }

            if (cantidadesCero==true || cantidadesdeMas == true)
            {
                cantidadesCeroOdeMas = true;
            }

            return cantidadesCeroOdeMas;
        }

        public ActionResult HistorialDonaciones()
        {
            var idUsuario = GetIdUsuario();

            var result = _servicioDonaciones.GetHistorialDonaciones(idUsuario);

            var viewModel = new HistorialDonacionesViewModel
            {
                TitulosTabla = GenerarTitulosTablaHistorialDonaciones(),
                ContenidoTabla = GenerarContenidoTablaHistorialDonaciones(result)
            };

            return View("~/Views/Donacion/HistorialDonaciones.cshtml", viewModel);
        }

        private IEnumerable<string> GenerarTitulosTablaHistorialDonaciones()
        {
            return new List<string>
            {
                "Fecha de donación",
                "Nombre",
                "Estado",
                "Total recaudado",
                "Mi donación",
                "Detalle necesidad"
            };
        }

        private IEnumerable<IEnumerable<string>> GenerarContenidoTablaHistorialDonaciones(IEnumerable<HistorialDonaciones> registrosHistoriales)
        {
            var contenido = new List<List<string>>();

            foreach (var registro in registrosHistoriales)
            {
                contenido.Add(
                    new List<string>
                    {
                        registro.FechaDonacion.ToString("dd/MM/yyyy"),
                        registro.NombreNecesidad,
                        registro.Estado,
                        registro.TotalRecaudado.ToString(),
                        registro.MiDonacion.ToString(),
                        $"<a href=\"/Necesidad/Detalle/{registro.IdNecesidad}\" class=\"btn btn-primary\">Detalle necesidad</a>"
                    });
            }

            return contenido;
        }
    }
}