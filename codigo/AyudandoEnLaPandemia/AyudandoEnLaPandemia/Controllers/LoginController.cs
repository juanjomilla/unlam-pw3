using Servicios;
using Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AyudandoEnLaPandemia.ViewModels;

namespace AyudandoEnLaPandemia.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
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

        [HttpGet]
        public ActionResult RegistroUsuario(String mensaje = "")
        {
            ViewBag.Message = mensaje;
            return View();
        }

        [HttpPost]
        public ActionResult RegistroUsuario(UsuariosViewModel registro)
        {

            if (!ModelState.IsValid)
            {
                return View(registro);
            }

            Usuarios usuarioNuevo = new Usuarios();

            usuarioNuevo.Nombre = registro.Nombre;
            usuarioNuevo.Apellido = registro.Apellido;
            usuarioNuevo.UserName = registro.UserName;
            usuarioNuevo.Email = registro.Email;
            usuarioNuevo.Password = registro.Password;
            usuarioNuevo.FechaNacimiento = Convert.ToDateTime(registro.FechaNacimiento);

            ServicioRegistrar.crearRegistro(usuarioNuevo);

            return View();
        }
      
    }
}