using Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class ServicioLogin
    {
        public static Usuarios ValidarLogin(Usuarios Usuario)
        {
            UsuarioRepositorio.BuscarUsuario(Usuario);

            Usuarios UsuarioEncontrado = UsuarioRepositorio.BuscarUsuario(Usuario);
            //UsuarioDB.Email = "jose@gmail.com";
            //UsuarioDB.Password = "2222"; 

            //if (String.Equals(Usuario.Email,UsuarioDB.Email) && String.Equals(Usuario.Password,UsuarioDB.Password))
            //{
            //    return true;
            //}
            return UsuarioEncontrado;
        }
    }
}
