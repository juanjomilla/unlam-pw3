using Repositorio;
using Repositorio.Repositorios;

namespace Servicios
{
    public class ServicioRegistrar
    {
        private IUsuarioRepositorio _usuarioRepositorio;

        // se arma el constructor y se guardan en variables privadas lo que inyecta autofac
        public ServicioRegistrar(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public void CrearRegistro(Usuarios usuarioNuevo)
        {
            _usuarioRepositorio.CrearUsuario(usuarioNuevo);
        }
    }
}