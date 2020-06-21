using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using Repositorio;
using Repositorio.Repositorios;

namespace Servicios
{
    public class ServicioNecesidad : IDisposable
    {
        private readonly UnitOfWork _unitOfWork;

        public ServicioNecesidad(Contexto contexto)
        {
            _unitOfWork = new UnitOfWork(contexto);
        }

        public IEnumerable<Necesidades> GetNecesidadesOtrosUsuarios(int idUsuario)
        {
            return _unitOfWork.Necesidades.Get(x => x.IdUsuarioCreador != idUsuario);
        }

        public IEnumerable<Necesidades> GetNecesidadesUsuario(int idUsuario)
        {
            return _unitOfWork.Necesidades.Get(x => x.IdUsuarioCreador == idUsuario);
        }

        public IEnumerable<Necesidades> GetNecesidadesMasValoradas(int top = 5)
        {
            return _unitOfWork.Necesidades.GetNecesidadesMasValoradas(top: top);
        }

        public void CrearNecesidad(Necesidades necesidad, HttpPostedFileBase imagen)
        {
            necesidad.Foto = GuardarImagen(necesidad.IdUsuarioCreador, imagen);

            _unitOfWork.Necesidades.Add(necesidad);

            _unitOfWork.SaveChanges();
        }

        public void Dispose()
        {
            if (_unitOfWork != null)
            {
                _unitOfWork.Dispose();
            }
        }

        private string GuardarImagen(int idUsuario, HttpPostedFileBase imagen)
        {
            var extension = Path.GetExtension(imagen.FileName);
            var nombreArchivo = $"{Guid.NewGuid().ToString().Substring(0, 10)}{extension}";
            var path = CrearCarpetaSiNoExiste(idUsuario);

            imagen.SaveAs($"{path}\\{nombreArchivo}");

            return nombreArchivo;
        }

        private string CrearCarpetaSiNoExiste(int idUsuario)
        {
            var path = HttpContext.Current.Server.MapPath($"~/Content/necesidades/imagenes/{idUsuario}");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }

        public Necesidades GetNecesidad(int idNecesidad)
        {
            return _unitOfWork.Necesidades.Get(idNecesidad);
        }
    }
}
