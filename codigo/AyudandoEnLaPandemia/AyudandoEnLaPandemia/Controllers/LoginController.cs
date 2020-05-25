using AyudandoEnLaPandemia.Models;
using Servicios;
using Repositorio;
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

            Usuarios usuario = new Usuarios();
            usuario.Email = login.email;
            usuario.Password = login.password;

            Usuarios usuarioEncontrado = ServicioLogin.ValidarLogin(usuario);

            if (usuarioEncontrado.IdUsuario<1)
            {
                
                return LoginUsuario("Email y/o Contraseña inválidos");
            }
            else {

                Session["UsuarioID"] = usuarioEncontrado.Email;

                return RedirectToAction("Index","Home");

            }
        }
    }
}