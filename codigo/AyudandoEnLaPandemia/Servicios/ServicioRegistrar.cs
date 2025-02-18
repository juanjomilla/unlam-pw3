﻿using Repositorio;
using Repositorio.Repositorios;
using System;
using System.Text;
using System.Web.Hosting;

namespace Servicios
{
    public class ServicioRegistrar
    {
        private IUsuarioRepositorio _usuarioRepositorio;
        private object emailEncontrado;

        // se arma el constructor y se guardan en variables privadas lo que inyecta autofac
        public ServicioRegistrar(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public void CrearRegistro(Usuarios usuarioNuevo)
        {
            _usuarioRepositorio.CrearUsuario(usuarioNuevo);
        }

        public StringBuilder CrearToken()
        {
            int longitud = 50;
            const string alfabeto = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            StringBuilder token = new StringBuilder();
            Random rnd = new Random();

            for (int i = 0; i < longitud; i++)
            {
                int indice = rnd.Next(alfabeto.Length);
                token.Append(alfabeto[indice]);
            }

            return token;
        }

        public void ValidarUsuario(int IdUsuario, string token)
        {
            _usuarioRepositorio.ValidarUsuario(IdUsuario, token);
        }

        public bool ValidarEmail(string email)
        {
            return _usuarioRepositorio.ValidarEmail(email);
        }
    }
}