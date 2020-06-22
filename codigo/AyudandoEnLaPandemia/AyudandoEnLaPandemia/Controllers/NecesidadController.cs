﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AyudandoEnLaPandemia.ViewModels;
using AyudandoEnLaPandemia.ViewModels.Necesidad;
using Repositorio;
using Servicios;

namespace AyudandoEnLaPandemia.Controllers
{
    public class NecesidadController : Controller
    {
        private readonly ServicioNecesidad _servicioNecesidad;
        private readonly ServicioValoraciones _servicioValoraciones;

        public NecesidadController(ServicioNecesidad servicioNecesidad, ServicioValoraciones servicioValoraciones)
        {
            _servicioNecesidad = servicioNecesidad;
            _servicioValoraciones = servicioValoraciones;
        }

        public ActionResult CrearNecesidad()
        {
            var viewModel = new CrearNecesidadViewModel
            {
                Form = new CrearNecesidadForm
                { 
                    Insumos = new List<InsumoForm> { new InsumoForm() },
                    Referencias = new List<ReferenciaForm> { new ReferenciaForm(), new ReferenciaForm() }
                }
            };

            return View("~/Views/Necesidad/crearNecesidad.cshtml", viewModel);
        }

        [HttpPost]
        public ActionResult GuardarNecesidad(
            [Bind(Include = "Form")] CrearNecesidadViewModel crearNecesidadViewModel,
            List<InsumoForm> insumos,
            List<ReferenciaForm> referencias,
            HttpPostedFileBase imagenNecesidad)
        {
            crearNecesidadViewModel.Form.Insumos = insumos ?? crearNecesidadViewModel.Form.Insumos;
            crearNecesidadViewModel.Form.Referencias = referencias ?? crearNecesidadViewModel.Form.Referencias;

            if (imagenNecesidad == null)
            {
                ModelState.AddModelError("ImagenEmpty", "Se debe adjuntar una imagen");
            }

            ValidarDatosForm(crearNecesidadViewModel.Form);

            if (!ModelState.IsValid)
            {
                return View("~/Views/Necesidad/crearNecesidad.cshtml", crearNecesidadViewModel);
            }

            var insumosList = new List<NecesidadesDonacionesInsumos>();
            var referenciasList = new List<NecesidadesReferencias>();

            foreach (var insumo in crearNecesidadViewModel.Form.Insumos)
            {
                insumosList.Add(new NecesidadesDonacionesInsumos { Nombre = insumo.Nombre, Cantidad = insumo.Cantidad });
            }

            foreach (var referencia in crearNecesidadViewModel.Form.Referencias)
            {
                referenciasList.Add(new NecesidadesReferencias { Nombre = referencia.Nombre, Telefono = referencia.Telefono });
            }

            var necesidad = new Necesidades
            {
                Nombre = crearNecesidadViewModel.Form.Nombre,
                Descripcion = crearNecesidadViewModel.Form.Descripcion,
                NecesidadesDonacionesInsumos = insumosList,
                NecesidadesReferencias = referenciasList,
                IdUsuarioCreador = (int) Session["UsuarioID"],
                TelefonoContacto = crearNecesidadViewModel.Form.TelefonoDeContacto,
                FechaFin = Convert.ToDateTime(crearNecesidadViewModel.Form.FechaFin),
                FechaCreacion = DateTime.Now
        };

            _servicioNecesidad.CrearNecesidad(necesidad, imagenNecesidad);

            return RedirectToAction("Home", "Home");
        }

        [HttpPost]
        public ActionResult AgregarInsumoPartial(
            [Bind(Include = "Form")] CrearNecesidadViewModel crearNecesidadViewModel,
            List<InsumoForm> insumos,
            List<ReferenciaForm> referencias)
        {
            var insumosList = insumos ?? crearNecesidadViewModel.Form.Insumos;
            var referenciasList = referencias ?? crearNecesidadViewModel.Form.Referencias;

            crearNecesidadViewModel.Form.Insumos = insumosList;
            crearNecesidadViewModel.Form.Referencias = referenciasList;
            crearNecesidadViewModel.Form.Insumos.Add(new InsumoForm());

            return PartialView("~/Views/Shared/Necesidad/_agregarInsumoPartial.cshtml", crearNecesidadViewModel.Form);
        }

        public ActionResult Detalle(int id, string mensaje = "")
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
                TituloPagina = necesidad.Nombre + " - Detalle de necesidad",
                Mensaje = mensaje
            };

            return View("~/Views/Necesidad/DetalleNecesidad.cshtml", viewModel);
        }

        public ActionResult ValorarNecesidad(int idNecesidad, bool valoracion)
        {
            // primero busco si el usuario ya valoró la necesidad
            var idUsuario = (int) Session["UsuarioID"];

            var necesidadValorada = _servicioValoraciones.NecesidadValorada(idNecesidad, idUsuario);

            if (necesidadValorada)
            {
                // la necesidad ya fue valorada por el usuario, debería retornar un mensaje para ser mostrado en la vista
                return RedirectToAction("Detalle", new { id = idNecesidad, mensaje = "Ya has valorado esta necesidad" });
            }

            _servicioValoraciones.ValorarNecesidad(idNecesidad, idUsuario, valoracion);

            return RedirectToAction("Detalle", new { id = idNecesidad, mensaje = "¡Valoración realizada correctamente!" });
        }

        private void ValidarDatosForm(CrearNecesidadForm form)
        {
            if (form.TipoDonacion == CrearNecesidadForm.TipoDeDonacion.Monetaria)
            {
                if (form.CantidadDinero < 1)
                {
                    ModelState.AddModelError("CantidadDinero", "La cantidad de dinero no puede ser menor a 1");
                }

                if (string.IsNullOrWhiteSpace(form.CBUAlias))
                {
                    ModelState.AddModelError("CBUAliasEmpty", "Se debe ingrear un CBU/Alias");
                }
            }
            else
            {
                if (!form.Insumos.Any())
                {
                    ModelState.AddModelError("InsumosEmpty", "Se debe ingresar como mínimo un insumo");
                }
            }

            if (form.Referencias.Count < 2)
            {
                ModelState.AddModelError("InsuficientesReferencias", "Se deben ingresar como mínimo dos referencias");
            }
        }
    }
}