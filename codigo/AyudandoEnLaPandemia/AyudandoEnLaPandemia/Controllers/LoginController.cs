﻿using System;
using System.Text;
using System.Web.Mvc;
using AyudandoEnLaPandemia.Const;
using AyudandoEnLaPandemia.ViewModels;
using Repositorio;
using Servicios;

namespace AyudandoEnLaPandemia.Controllers
{
    public class LoginController : BaseController
    {
        private readonly ServicioLogin _servicioLogin;
        private readonly ServicioRegistrar _servicioRegistrar;

        // se arma el constructor y se guardan en variables privadas lo que inyecta autofac
        public LoginController(ServicioLogin servicioLogin, ServicioRegistrar servicioRegistrar) 
        {
            _servicioLogin = servicioLogin;
            _servicioRegistrar = servicioRegistrar;
        }

        [HttpGet]
        public ActionResult LoginUsuario(String mensaje = "", String redirigir = "")
        {
            ViewBag.Message = mensaje;
            ViewBag.url = redirigir;

            return View();
        }

        [HttpPost]
        public ActionResult LoginUsuario(Usuarios login, String redirigir)
        {

            if (!ModelState.IsValid)
            {
                return View(login);
            }

            Usuarios usuarioEncontrado = _servicioLogin.ValidarLogin(login);

            if (usuarioEncontrado == null)
            {
                return LoginUsuario("Email y/o Contraseña inválidos");
            }
            else
            {
                if (!usuarioEncontrado.Activo)
                {
                    return LoginUsuario("Su usuario está inactivo. Actívelo desde el email recibido");
                }
                else
                { 
                    Session[Constantes.IdUsuarioSessionKey] = usuarioEncontrado.IdUsuario;
                    Session[Constantes.NombreApellidoUsuarioSessionKey] = usuarioEncontrado.Nombre+" "+usuarioEncontrado.Apellido;
                    Session[Constantes.EsAdminSessionKey] = usuarioEncontrado.TipoUsuario == 1;

                    if (!string.IsNullOrWhiteSpace(redirigir))
                    {
                        return Redirect(redirigir);
                    }
                    else
                    {
                        return RedirectToAction("Home", "Home");
                    }
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

            bool emailEncontrado = _servicioRegistrar.ValidarEmail(registro.Email);

            if (emailEncontrado)
            {
                return RegistroUsuario("NotOK");
            }
            else
            { 
                StringBuilder token = _servicioRegistrar.CrearToken();

                Usuarios usuarioNuevo = new Usuarios();

                usuarioNuevo.Email = registro.Email;
                usuarioNuevo.Password = registro.Password;
                usuarioNuevo.FechaNacimiento = registro.FechaNacimiento;
                usuarioNuevo.TipoUsuario = 0; //Usuario normal
                usuarioNuevo.FechaCreacion= DateTime.Today;
                usuarioNuevo.Activo = false;
                usuarioNuevo.Token = token.ToString();
            
                _servicioRegistrar.CrearRegistro(usuarioNuevo);

                return RegistroUsuario("OK");
            }
        }

        public ActionResult Confirm(int IdUsuario, string token, String mensaje = "")
        {
            ViewBag.Message = mensaje;
            ViewBag.IdUsuario = IdUsuario;
            ViewBag.Token = token;
            return View();
        }
        
        public ActionResult RegisterConfirm(int IdUsuario, string Token)
        {
            _servicioRegistrar.ValidarUsuario(IdUsuario, Token);

            return View();
        }

    }
}