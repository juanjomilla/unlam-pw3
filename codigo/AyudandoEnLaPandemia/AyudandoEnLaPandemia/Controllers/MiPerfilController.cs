using Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Servicios;
using AyudandoEnLaPandemia.ViewModels.MiPerfil;

namespace AyudandoEnLaPandemia.Controllers
{
    public class MiPerfilController : Controller
    {
        private readonly ServicioLogin _servicioLogin;

        public MiPerfilController(ServicioLogin servicioLogin)
        {
            _servicioLogin = servicioLogin;
        }


        [HttpGet]
        public ActionResult MiPerfil()
        {
            var idUsuario = (int)Session["UsuarioID"];

            Usuarios perfil = _servicioLogin.ObtenerPerfil(idUsuario);

            var perfilActual = new MiPerfilViewModel
            {
                Nombre = perfil.Nombre,
                Apellido = perfil.Apellido,
                FechaNacimiento = perfil.FechaNacimiento,
                UserName = perfil.UserName,
                Foto = perfil.Foto,
                IdUsuario = perfil.IdUsuario
            };
            return View("~/Views/MiPerfil/MiPerfil.cshtml", perfilActual);
        }

        [HttpPost]
        public ActionResult MiPerfil(MiPerfilViewModel perfil, HttpPostedFileBase foto)
        {

            int idUsuario = (int)Session["UsuarioID"];

            if (foto == null)
            {
                ModelState.AddModelError("FotoEmpty", "Se debe adjuntar foto");
            }

            if (!ModelState.IsValid)
            {
                return View(perfil);
            }

            perfil.Foto = _servicioLogin.GuardarAdjunto(idUsuario, foto);

            _servicioLogin.ActualizarPerfil(perfil.Nombre, perfil.Apellido, perfil.FechaNacimiento, perfil.Foto, idUsuario);

            return RedirectToAction("MiPerfil");
        }
    }
}