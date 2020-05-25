using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio
{
    public class UsuarioRepositorio
    {
        
        public static Usuarios BuscarUsuario(Usuarios usuario)
        {
            Contexto context = new Contexto();
            var UsuarioEncontrado =context.Usuarios.Find(usuario.Email);
            return UsuarioEncontrado;
        }
    }
}
