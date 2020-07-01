using Repositorio;
using Repositorio.Repositorios;
using System.Security.Cryptography.X509Certificates;

namespace Servicios
{
    public class ServicioLogin
    {
        private IUsuarioRepositorio _usuarioRepositorio;

        // se arma el constructor y se guardan en variables privadas lo que inyecta autofac
        public ServicioLogin(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public Usuarios ValidarLogin(Usuarios Usuario)
        {
            Usuarios UsuarioEncontrado = _usuarioRepositorio.BuscarUsuario(Usuario);
       
            return UsuarioEncontrado;
        }

        public Usuarios ObtenerUsuario(int idUsuario)
        {
            return _usuarioRepositorio.Get(idUsuario);
        }

        public bool UsuarioConPerfilCompleto(int idUsuario)
        {
            var usuario = _usuarioRepositorio.Get(idUsuario);

            return !string.IsNullOrWhiteSpace(usuario.Apellido) &&
                !string.IsNullOrWhiteSpace(usuario.Nombre) &&
                !string.IsNullOrWhiteSpace(usuario.Foto) &&
                usuario.FechaNacimiento != null;
        }

        public bool EsAdministrador(int idUsuario)
        {
            var usuario = _usuarioRepositorio.Get(idUsuario);

            return usuario.TipoUsuario == 1;
        }
    }
}
