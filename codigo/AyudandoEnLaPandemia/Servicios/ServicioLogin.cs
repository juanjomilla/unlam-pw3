using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class ServicioLogin
    {
        public static bool ValidarLogin(Usuario Usuario)
        {

            Usuario UsuarioDB = new Usuario();
            UsuarioDB.Email = "jose@gmail.com";
            UsuarioDB.Password = "2222";

            if (String.Equals(Usuario.Email,UsuarioDB.Email) && String.Equals(Usuario.Password,UsuarioDB.Password))
            {
                return true;
            }
            return false;
        }
    }
}
