using System;
using System.Linq;

namespace Repositorio.Repositorios
{
    public class UsuarioRepositorio : Repository<Usuarios>, IUsuarioRepositorio
    {
        // El contexto ya está registrado en autofac, por lo tanto se inyecta automáticamente
        public UsuarioRepositorio(Contexto contexto) : base(contexto) { }
        
        public Usuarios BuscarUsuario(Usuarios usuario)
        {
            var usuarioEcontrado = Get(x => x.Email == usuario.Email && x.Password == usuario.Password).FirstOrDefault();
            //var usuarioEcontrado = (from u in _dbContext.Usuarios 
            //                        where u.Email == usuario.Email && 
            //                        u.Password == usuario.Password 
            //                        select u).FirstOrDefault<Usuarios>();

            return usuarioEcontrado;

        }

        public void CrearUsuario(Usuarios usuarioNuevo)
        {
            using (var unitOfWork = new UnitOfWork(_dbContext))
            {
                _dbContext.Usuarios.Add(usuarioNuevo);

                try
                {
                    _dbContext.SaveChanges();
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
