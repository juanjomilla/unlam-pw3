using AyudandoEnLaPandemia.Models;
using Entidades;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AyudandoEnLaPandemia.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult LoginUsuario(String mensaje = "")
        {
            ViewBag.Message = mensaje;
            return View();
        }

        [HttpPost]
        public ActionResult LoginUsuario(FormularioLogin login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            Usuario usuario = new Usuario();
            usuario.Email = login.email;
            usuario.Password = login.password;

            bool status = ServicioLogin.ValidarLogin(usuario);

            if (!status)
            {
                //TempData["datosInvalidos"] = true;
                //return Redirect("/");
                //return View("mal","mal");
                return LoginUsuario("Usuario o contraseña inválido");
            }

            return View();
        }
    }
}