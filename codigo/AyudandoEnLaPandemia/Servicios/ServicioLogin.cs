using Repositorio;
using Repositorio.Repositorios;
using System;
using System.IO;
using System.Web;
using System.Security.Cryptography.X509Certificates;

namespace Servicios
{
    public class ServicioLogin : IDisposable
    {
        //private IUsuarioRepositorio _usuarioRepositorio;
        private readonly UnitOfWork _unitOfWork;

        // se arma el constructor y se guardan en variables privadas lo que inyecta autofac
        public ServicioLogin(Contexto contexto)
        {
            _unitOfWork = new UnitOfWork(contexto);
        }

        public Usuarios ValidarLogin(Usuarios Usuario)
        {
            Usuarios UsuarioEncontrado = _unitOfWork.Usuarios.BuscarUsuario(Usuario);
       
            return UsuarioEncontrado;
        }

        public Usuarios ObtenerPerfil(int idUsuario)
        {
            return _unitOfWork.Usuarios.Get(idUsuario);
        }

        public bool UsuarioConPerfilCompleto(int idUsuario)
        {
            var usuario = _unitOfWork.Usuarios.Get(idUsuario);

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

        public string CrearUserName(string nombre, string apellido)
        {
            string possibleUserName = $"{nombre}.{apellido}";
            string username = _unitOfWork.Usuarios.VerificarUserName(possibleUserName, nombre, apellido);
            return username;
        }

        public void ActualizarPerfil(string nombre, string apellido, DateTime fechaNacimiento, string foto, int idUsuario, string userName)
        {
            _unitOfWork.Usuarios.ActualizarPerfil(nombre, apellido, fechaNacimiento, foto, idUsuario, userName);
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
            var usuario = _unitOfWork.Usuarios.Get(idUsuario);

            return usuario.TipoUsuario == 1;
        }

        public void Dispose()
        {
            if (_unitOfWork != null)
            {
                _unitOfWork.Dispose();
            }
        }
    }
}
