using System;
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
        private readonly ServicioLogin _servicioLogin;

        public NecesidadController(
            ServicioNecesidad servicioNecesidad,
            ServicioValoraciones servicioValoraciones,
            ServicioLogin servicioLogin)
        {
            _servicioNecesidad = servicioNecesidad;
            _servicioValoraciones = servicioValoraciones;
            _servicioLogin = servicioLogin;
        }

        public ActionResult CrearNecesidad()
        {
            var viewModel = new CrearNecesidadViewModel
            {
                Insumos = new List<InsumoForm> { new InsumoForm() },
                Referencias = new List<ReferenciaForm> { new ReferenciaForm(), new ReferenciaForm() }
            };

            var idUsuario = (int) Session["UsuarioID"];

            ValidarMaximoNecesidades(idUsuario);
            ValidarUsuarioActivo(idUsuario);

            return View("~/Views/Necesidad/crearNecesidad.cshtml", viewModel);
        }

        [HttpPost]
        public ActionResult GuardarNecesidad(
            CrearNecesidadViewModel crearNecesidadViewModel,
            List<InsumoForm> insumos,
            List<ReferenciaForm> referencias,
            HttpPostedFileBase foto)
        {
            crearNecesidadViewModel.Insumos = insumos ?? crearNecesidadViewModel.Insumos;
            crearNecesidadViewModel.Referencias = referencias ?? crearNecesidadViewModel.Referencias;

            if (foto == null)
            {
                ModelState.AddModelError("ImagenEmpty", "Se debe adjuntar una imagen");
            }

            var idUsuario = (int)Session["UsuarioID"];

            ValidarMaximoNecesidades(idUsuario);
            ValidarUsuarioActivo(idUsuario);
            ValidarDatosForm(crearNecesidadViewModel);

            if (!ModelState.IsValid)
            {
                return View("~/Views/Necesidad/crearNecesidad.cshtml", crearNecesidadViewModel);
            }

            var insumosList = new List<NecesidadesDonacionesInsumos>();
            var referenciasList = new List<NecesidadesReferencias>();
            var monetaria = new List<NecesidadesDonacionesMonetarias>();
            monetaria.Add(new NecesidadesDonacionesMonetarias
            {
                CBU = crearNecesidadViewModel.CBUAlias,
                Dinero = crearNecesidadViewModel.CantDinero
            });

            foreach (var insumo in crearNecesidadViewModel.Insumos)
            {
                insumosList.Add(new NecesidadesDonacionesInsumos { Nombre = insumo.Nombre, Cantidad = insumo.Cantidad });
            }

            foreach (var referencia in crearNecesidadViewModel.Referencias)
            {
                referenciasList.Add(new NecesidadesReferencias { Nombre = referencia.Nombre, Telefono = referencia.Telefono });
            }

            var necesidad = new Necesidades
            {
                Nombre = crearNecesidadViewModel.Nombre,
                Descripcion = crearNecesidadViewModel.Descripcion,
                NecesidadesReferencias = referenciasList,
                IdUsuarioCreador = idUsuario,
                TelefonoContacto = crearNecesidadViewModel.TelefonoContacto,
                FechaFin = Convert.ToDateTime(crearNecesidadViewModel.FechaFin),
                FechaCreacion = DateTime.Now
            };

            if (crearNecesidadViewModel.TipoDonacion == CrearNecesidadViewModel.TipoDeDonacion.Insumos)
            {
                necesidad.NecesidadesDonacionesInsumos = insumosList;
            }
            else
            {
                necesidad.NecesidadesDonacionesMonetarias = monetaria;
            }   

            _servicioNecesidad.CrearNecesidad(necesidad, foto);

            return RedirectToAction("Home", "Home");
        }

        [HttpPost]
        public ActionResult AgregarInsumoPartial(
            [Bind(Include = "Form")] CrearNecesidadViewModel crearNecesidadViewModel,
            List<InsumoForm> insumos,
            List<ReferenciaForm> referencias)
        {
            var insumosList = insumos ?? crearNecesidadViewModel.Insumos;
            var referenciasList = referencias ?? crearNecesidadViewModel.Referencias;

            crearNecesidadViewModel.Insumos = insumosList;
            crearNecesidadViewModel.Referencias = referenciasList;
            crearNecesidadViewModel.Insumos.Add(new InsumoForm());

            return PartialView("~/Views/Shared/Necesidad/_agregarInsumoPartial.cshtml", crearNecesidadViewModel);
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

        private void ValidarMaximoNecesidades(int idUsuario)
        {
            var limiteNecesidades = 3;
            var cantNecesidades = _servicioNecesidad.GetNecesidadesUsuario(idUsuario).Count();

            if (cantNecesidades >= limiteNecesidades)
            {
                ModelState.AddModelError("LimiteNecesidades", $"Se ha alcanzado el límite de {limiteNecesidades} necesidades por usuario");
            }
        }

        private void ValidarUsuarioActivo(int idUsuario)
        {
            if (!_servicioLogin.UsuarioConPerfilCompleto(idUsuario))
            {
                ModelState.AddModelError("PerfilIncompleto", "Debe completar el perfil antes de poder crear una necesidad. Ir a {0}");
            }
        }

        private void ValidarDatosForm(CrearNecesidadViewModel form)
        {
            if (form.TipoDonacion == CrearNecesidadViewModel.TipoDeDonacion.Monetaria)
            {
                if (form.CantDinero < 1)
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