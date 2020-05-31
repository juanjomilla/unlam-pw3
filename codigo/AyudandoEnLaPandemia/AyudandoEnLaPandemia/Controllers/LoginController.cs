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
        public ActionResult LoginUsuario(Usuarios login)
        {

            if (!ModelState.IsValid)
            {
                return View(login);
            }

            Usuarios usuarioEncontrado = ServicioLogin.ValidarLogin(login);

            if ( usuarioEncontrado == null)
            {
                return LoginUsuario("Email y/o Contraseña inválidos");
            }
            else {
                if (!usuarioEncontrado.Activo)
                {
                    return LoginUsuario("Su usuario está inactivo. Actívelo desde el email recibido");
                }
                else { 
                    Session["UsuarioID"] = usuarioEncontrado.IdUsuario;
                    Session["UsuarioNombreApellido"] = usuarioEncontrado.Nombre+" "+usuarioEncontrado.Apellido;

                    return RedirectToAction("Index","Home");
                }
            }
        }
        public ActionResult Salir()
        {
            Session.Abandon();
            return Redirect("/Login/LoginUsuario");
        }
    }
}