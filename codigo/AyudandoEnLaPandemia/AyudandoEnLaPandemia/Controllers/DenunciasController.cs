﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AyudandoEnLaPandemia.ViewModels.Denuncias;
using Repositorio;
using Servicios;

namespace AyudandoEnLaPandemia.Controllers
{
    public class DenunciasController : Controller
    {
        private readonly ServicioLogin _servicioLogin;
        private readonly ServicioDenuncias _servicioDenuncias;
        private readonly ServicioNecesidad _servicioNecesidad;

        public DenunciasController(
            ServicioLogin servicioLogin,
            ServicioDenuncias servicioDenuncias,
            ServicioNecesidad servicioNecesidad)
        {
            _servicioLogin = servicioLogin;
            _servicioDenuncias = servicioDenuncias;
            _servicioNecesidad = servicioNecesidad;
        }

        [HttpGet]
        public ActionResult DenunciarNecesidad(int idNecesidad = 0)
        {
            ViewBag.TodosLosMotivos = _servicioDenuncias.ObtenerMotivosDenuncias();
            ViewBag.IdNecesidad = idNecesidad;
            return View();
        }

        [HttpPost]
        public ActionResult DenunciarNecesidad(Denuncias nuevaDenuncia)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.TodosLosMotivos = _servicioDenuncias.ObtenerMotivosDenuncias();
                return View(nuevaDenuncia);
            }

            var idUsuario = (int)Session["UsuarioID"];

            var usuario = _servicioLogin.ObtenerPerfil(idUsuario);

            var necesidad = _servicioNecesidad.GetNecesidad(nuevaDenuncia.IdNecesidad);

            var motivoDenuncia = _servicioDenuncias.ObtenerMotivoDenuncia(nuevaDenuncia.MotivoDenuncia.IdMotivoDenuncia);

            if (usuario == null || necesidad == null || motivoDenuncia == null)
            {
                return RedirectToAction("Detalle", "Necesidad", new { id = nuevaDenuncia.IdNecesidad, mensaje = "Ha ocurrido un error." });
            }

            var necesidadDenunciada = _servicioDenuncias.NecesidadDenunciada(necesidad, usuario);

            if (necesidadDenunciada)
            {
                return RedirectToAction("Detalle", "Necesidad", new { id = nuevaDenuncia.IdNecesidad, mensaje = "Ya has denunciado esta necesidad." });
            }

            var denuncia = new Denuncias
            {
                Necesidades = necesidad,
                MotivoDenuncia = motivoDenuncia,
                Comentarios = nuevaDenuncia.Comentarios,
                Usuarios = usuario,
                FechaCreacion = DateTime.Now,
                Estado = 0
            };

            _servicioDenuncias.CrearDenuncia(denuncia);

            return RedirectToAction("Detalle", "Necesidad", new { id = nuevaDenuncia.IdNecesidad, mensaje = "¡Necesidad denunciada con éxito!" });
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

            var denuncias = _servicioDenuncias.ObtenerDenunciasActivas();

            var viewModel = new GestionDenunciasViewModel
            {
                TitulosTabla = GenerarTitulosTablaDenuncias(),
                ContenidoTabla = GenerarContenidoTablaDenuncias(denuncias)
            };

            return View("~/Views/Denuncias/GestionDenuncias.cshtml", viewModel);
        }

        private IEnumerable<string> GenerarTitulosTablaDenuncias()
        {
            return new List<string>
            {
                "Fecha de creación",
                "Motivo",
                "Detalle de necesidad",
                "Comentarios",
                string.Empty,
                string.Empty
            };
        }

        private IEnumerable<IEnumerable<string>> GenerarContenidoTablaDenuncias(IEnumerable<Denuncias> registrosDenuncias)
        {
            var contenido = new List<List<string>>();

            foreach (var registro in registrosDenuncias)
            {
                contenido.Add(
                    new List<string>
                    {
                        registro.FechaCreacion.ToString("dd MMMM yyyy"),
                        registro.MotivoDenuncia.Descripcion,
                        $"<a href=\"/Necesidad/Detalle/{registro.IdNecesidad}\" class=\"btn btn-primary\">Detalle necesidad</a>",
                        registro.Comentarios,
                        $"<a href=\"/Denuncias/DesestimarDenuncia/{registro.IdDenuncia}\" class=\"btn btn-primary\">Desestimar denuncia</a>",
                        $"<a href=\"/Denuncias/AceptarDenuncia/{registro.IdDenuncia}\" class=\"btn btn-primary\">Aceptar denuncia</a>"
                    });
            }

            return contenido;
        }
    }
}