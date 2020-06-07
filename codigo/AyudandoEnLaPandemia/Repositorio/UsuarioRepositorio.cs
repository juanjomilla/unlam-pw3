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

            var usuarioEcontrado = (from u in context.Usuarios 
                                    where u.Email == usuario.Email && 
                                    u.Password == usuario.Password 
                                    select u).FirstOrDefault<Usuarios>();

            return usuarioEcontrado;

        }

        public static void crearUsuario(Usuarios usuarioNuevo)
        {
            Contexto context = new Contexto();

            context.Usuarios.Add(usuarioNuevo);
            //context.SaveChanges();

           try { 
            context.SaveChanges();
            }
            catch(Exception ex)
            {

            }
        }


    }
}
