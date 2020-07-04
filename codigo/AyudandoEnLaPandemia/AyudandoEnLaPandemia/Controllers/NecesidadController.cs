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
    public class NecesidadController : BaseController
    {
        private readonly ServicioNecesidad _servicioNecesidad;
        private readonly ServicioValoraciones _servicioValoraciones;
        private readonly ServicioLogin _servicioLogin;
        private readonly ServicioDonaciones _servicioDonaciones;

        public NecesidadController(
            ServicioNecesidad servicioNecesidad,
            ServicioValoraciones servicioValoraciones,
            ServicioLogin servicioLogin,
            ServicioDonaciones servicioDonaciones)
        {
            _servicioNecesidad = servicioNecesidad;
            _servicioValoraciones = servicioValoraciones;
            _servicioLogin = servicioLogin;
            _servicioDonaciones = servicioDonaciones;
        }

        public ActionResult CrearNecesidad()
        {
            var viewModel = new GuardarNecesidadViewModel
            {
                Insumos = new List<NecesidadesDonacionesInsumos> { new NecesidadesDonacionesInsumos() },
                Referencias = new List<NecesidadesReferencias> { new NecesidadesReferencias(), new NecesidadesReferencias() }
            };

            var idUsuario = GetIdUsuario();

            ValidarMaximoNecesidades(idUsuario);
            ValidarUsuarioActivo(idUsuario);

            return View("~/Views/Necesidad/guardarNecesidad.cshtml", viewModel);
        }

        public ActionResult ModificarNecesidad(int idNecesidad = 0)
        {
            var necesidad = _servicioNecesidad.GetNecesidad(idNecesidad);
            var idUsuario = GetIdUsuario();

            if (idUsuario != necesidad.IdUsuarioCreador)
            {
                SetMensajeError("No puede modificar la necesidad si no es el usuario creador de la misma");
                return RedirectToAction("Home", "Home");
            }

            var viewModel = new GuardarNecesidadViewModel
            {
                Insumos = necesidad.NecesidadesDonacionesInsumos,
                Referencias = necesidad.NecesidadesReferencias,
                Descripcion = necesidad.Descripcion,
                FechaFin = necesidad.FechaFin,
                Nombre = necesidad.Nombre,
                TelefonoContacto = necesidad.TelefonoContacto,
                TipoDonacion = (GuardarNecesidadViewModel.TipoDeDonacion) necesidad.TipoDonacion,
                ModificandoDatos = true,
                IdNecesidad = idNecesidad,
                TituloPagina = "Modificar necesidad"
            };

            if (necesidad.NecesidadesDonacionesMonetarias.Any())
            {
                viewModel.CantDinero = necesidad.NecesidadesDonacionesMonetarias.First().Dinero;
                viewModel.CBUAlias = necesidad.NecesidadesDonacionesMonetarias.First().CBU;
            }

            return View("~/Views/Necesidad/guardarNecesidad.cshtml", viewModel);
        }

        [HttpPost]
        public ActionResult GuardarNecesidad(GuardarNecesidadViewModel crearNecesidadViewModel, HttpPostedFileBase foto)
        {
            if (foto == null)
            {
                ModelState.AddModelError("ImagenEmpty", "Se debe adjuntar una imagen.");
            }

            var idUsuario = GetIdUsuario();

            ValidarMaximoNecesidades(idUsuario);
            ValidarUsuarioActivo(idUsuario);
            ValidarDatosForm(crearNecesidadViewModel);

            if (!ModelState.IsValid)
            {
                return View("~/Views/Necesidad/guardarNecesidad.cshtml", crearNecesidadViewModel);
            }

            var usuario = _servicioLogin.ObtenerPerfil(idUsuario);
            var monetaria = new List<NecesidadesDonacionesMonetarias>
            {
                new NecesidadesDonacionesMonetarias
                {
                    CBU = crearNecesidadViewModel.CBUAlias,
                    Dinero = crearNecesidadViewModel.CantDinero
                }
            };

            var necesidad = new Necesidades
            {
                Nombre = crearNecesidadViewModel.Nombre,
                Descripcion = crearNecesidadViewModel.Descripcion,
                TelefonoContacto = crearNecesidadViewModel.TelefonoContacto,
                FechaFin = Convert.ToDateTime(crearNecesidadViewModel.FechaFin),
                FechaCreacion = DateTime.Now,
                TipoDonacion = (int)crearNecesidadViewModel.TipoDonacion,
                Usuarios = usuario,
                IdUsuarioCreador = idUsuario,
                Valoracion = 0
            };

            _servicioNecesidad.CrearNecesidad(
                crearNecesidadViewModel.Referencias,
                monetaria,
                crearNecesidadViewModel.Insumos,
                necesidad,
                foto);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult ActualizarNecesidad(
            GuardarNecesidadViewModel crearNecesidadViewModel,
            HttpPostedFileBase foto)
        {
            var necesidad = new Necesidades
            {
                Nombre = crearNecesidadViewModel.Nombre,
                Descripcion = crearNecesidadViewModel.Descripcion,
                TelefonoContacto = crearNecesidadViewModel.TelefonoContacto,
                FechaFin = Convert.ToDateTime(crearNecesidadViewModel.FechaFin),
                FechaCreacion = DateTime.Now,
                TipoDonacion = (int)crearNecesidadViewModel.TipoDonacion
            };

            if (!ValidarDatosForm(crearNecesidadViewModel))
            {
                return View("~/Views/Necesidad/guardarNecesidad.cshtml", crearNecesidadViewModel);
            }

            var necesidadesMonetarias = new List<NecesidadesDonacionesMonetarias>
            {
                new NecesidadesDonacionesMonetarias
                {
                    CBU = crearNecesidadViewModel.CBUAlias,
                    Dinero = crearNecesidadViewModel.CantDinero
                }
            };

            necesidad.NecesidadesDonacionesInsumos = crearNecesidadViewModel.Insumos;
            necesidad.NecesidadesDonacionesMonetarias = necesidadesMonetarias;
            necesidad.NecesidadesReferencias = crearNecesidadViewModel.Referencias;

            _servicioNecesidad.ActualizarNecesidad(
                necesidad,
                foto,
                crearNecesidadViewModel.IdNecesidad);

            return RedirectToAction("Home", "Home");
        }

        [HttpPost]
        public ActionResult AgregarInsumoPartial(GuardarNecesidadViewModel crearNecesidadViewModel)
        {
            crearNecesidadViewModel.Insumos.Add(new NecesidadesDonacionesInsumos());

            return PartialView("~/Views/Shared/Necesidad/_agregarInsumoPartial.cshtml", crearNecesidadViewModel);
        }

        public ActionResult Detalle(int id, string mensaje = "")
        {
            var necesidad = _servicioNecesidad.GetNecesidad(id);

            if (necesidad == null)
            {
                return HttpNotFound();
            }

            var detalleTotalDonacion = necesidad.TipoDonacion == 0 ?
                necesidad.NecesidadesDonacionesMonetarias
                    .Select(x => new KeyValuePair<string, decimal>("Dinero", _servicioDonaciones.GetTotalDonacionesMonetaria(x.IdNecesidadDonacionMonetaria))) :
                necesidad.NecesidadesDonacionesInsumos
                    .Select(x => new KeyValuePair<string, decimal>(x.Nombre, _servicioDonaciones.GetTotalDonacionesInsumo(x.IdNecesidadDonacionInsumo)));

            var viewModel = new DetalleNecesidadViewModel
            {
                Necesidad = necesidad,
                EsPropietario = necesidad.IdUsuarioCreador == GetIdUsuario(),
                TituloPagina = necesidad.Nombre + " - Detalle de necesidad",
                Mensaje = mensaje,
                DetalleTotalDonacion = detalleTotalDonacion
            };

            return View("~/Views/Necesidad/DetalleNecesidad.cshtml", viewModel);
        }

        public ActionResult ValorarNecesidad(int idNecesidad, bool valoracion)
        {
            // primero busco si el usuario ya valoró la necesidad
            var idUsuario = GetIdUsuario();

            var necesidadValorada = _servicioValoraciones.NecesidadValorada(idNecesidad, idUsuario);

            if (necesidadValorada)
            {
                // la necesidad ya fue valorada por el usuario, debería retornar un mensaje para ser mostrado en la vista
                return RedirectToAction("Detalle", new { id = idNecesidad, mensaje = "ERROR: Ya has valorado esta necesidad." });
            }

            _servicioValoraciones.ValorarNecesidad(idNecesidad, idUsuario, valoracion);

            return RedirectToAction("Detalle", new { id = idNecesidad, mensaje = "¡Valoración realizada correctamente!" });
        }

        [HttpPost]
        public ActionResult BuscarNecesidad(string busqueda)
        {
            var idUsuario = GetIdUsuario();
            var resultadoBusqueda = _servicioNecesidad.BuscarNecesidades(busqueda, idUsuario);

            var viewModel = new NecesidadViewModel
            {
                Necesidades = resultadoBusqueda,
                TituloPagina = "Resultados búsqueda"
            };

            return PartialView("~/Views/Shared/_necesidadPartial.cshtml", viewModel);
        }

        private void ValidarMaximoNecesidades(int idUsuario)
        {
            var limiteNecesidades = 3;
            var cantNecesidades = _servicioNecesidad.GetNecesidadesUsuario(idUsuario).Count();

            if (cantNecesidades >= limiteNecesidades)
            {
                ModelState.AddModelError("LimiteNecesidades", $"Se ha alcanzado el límite de {limiteNecesidades} necesidades por usuario.");
            }
        }

        private void ValidarUsuarioActivo(int idUsuario)
        {
            if (!_servicioLogin.UsuarioConPerfilCompleto(idUsuario))
            {
                ModelState.AddModelError("PerfilIncompleto", "Debe completar el perfil antes de poder crear una necesidad. Ir a {0}");
            }
        }

        public void ValidarImagenNoEsNula(HttpPostedFileBase foto)
        {
            if (foto == null)
            {
                ModelState.AddModelError("ImagenEmpty", "Se debe adjuntar una imagen.");
            }
        }

        private bool ValidarDatosForm(GuardarNecesidadViewModel form)
        {
            var datosValidos = true;

            if (form.TipoDonacion == GuardarNecesidadViewModel.TipoDeDonacion.Monetaria)
            {
                if (form.CantDinero < 1)
                {
                    ModelState.AddModelError("CantidadDinero", "La cantidad de dinero no puede ser menor a 1.");
                    datosValidos = false;
                }

                if (string.IsNullOrWhiteSpace(form.CBUAlias))
                {
                    ModelState.AddModelError("CBUAliasEmpty", "Se debe ingresar un CBU/Alias.");
                    datosValidos = false;
                }
            }
            else
            {
                if (!form.Insumos.Any())
                {
                    ModelState.AddModelError("InsumosEmpty", "Se debe ingresar como mínimo un insumo.");
                    datosValidos = false;
                }
            }

            if (form.Referencias.Count < 2)
            {
                ModelState.AddModelError("InsuficientesReferencias", "Se deben ingresar como mínimo dos referencias.");
                datosValidos = false;
            }

            return datosValidos;
        }
    }
}