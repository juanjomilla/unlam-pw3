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
            
            Usuarios UsuarioEncontrado = UsuarioRepositorio.BuscarUsuario(Usuario);
       
            return UsuarioEncontrado;
        }
    }
}
