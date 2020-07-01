using Repositorio;
using Repositorio.Repositorios;
using System;
using System.IO;
using System.Web;

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

        public Usuarios ObtenerPerfil(int idUsuario)
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

        public string GuardarAdjunto(int idUsuario, HttpPostedFileBase foto)
        {
            var extension = Path.GetExtension(foto.FileName);
            var nombreArchivo = $"{Guid.NewGuid().ToString().Substring(0, 10)}{extension}";
            var path = CrearCarpetaSiNoExiste(idUsuario);

            foto.SaveAs($"{path}\\{nombreArchivo}");

            return nombreArchivo;
        }

        public void ActualizarPerfil(string nombre, string apellido, DateTime fechaNacimiento, string foto, int idUsuario)
        {
            _usuarioRepositorio.ActualizarPerfil(nombre, apellido, fechaNacimiento, foto, idUsuario);
        }

        private object CrearCarpetaSiNoExiste(int idUsuario)
        {
            var path = HttpContext.Current.Server.MapPath($"~/Content/usuario/imagenes/{idUsuario}");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }

        public bool EsAdministrador(int idUsuario)
        {
            var usuario = _usuarioRepositorio.Get(idUsuario);

            return usuario.TipoUsuario == 1;
        }
    }
}
