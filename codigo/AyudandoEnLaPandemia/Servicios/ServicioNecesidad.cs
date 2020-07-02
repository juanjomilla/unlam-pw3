using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Repositorio;

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
            return _unitOfWork.Necesidades.Get(x => x.IdUsuarioCreador != idUsuario && (x.Estado != 3 && x.Estado != 4));
        }

        public IEnumerable<Necesidades> GetNecesidadesUsuario(int idUsuario)
        {
            return _unitOfWork.Necesidades.Get(x => x.IdUsuarioCreador == idUsuario && (x.Estado != 3 && x.Estado != 4));
        }

        public int GetTipoNecesidad(int idNecesidad)
        {
            return _unitOfWork.Necesidades.GetTipoNecesidad(idNecesidad);
        }

        public IEnumerable<Necesidades> GetNecesidadesMasValoradas(int top = 5)
        {
            return _unitOfWork.Necesidades.GetNecesidadesMasValoradas(top: top);
        }

        public Necesidades GetNecesidad(int idNecesidad)
        {
            return _unitOfWork.Necesidades.Get(idNecesidad);
        }

        public IEnumerable<Necesidades> BuscarNecesidades(string buscar)
        {
            return _unitOfWork.Necesidades.BuscarNecesidades(buscar);
        }

        public void CrearNecesidad(
            ICollection<NecesidadesReferencias> necesidadesReferencias,
            ICollection<NecesidadesDonacionesMonetarias> necesidadesDonacionesMonetarias,
            ICollection<NecesidadesDonacionesInsumos> necesidadesDonacionesInsumos,
            Necesidades necesidad,
            HttpPostedFileBase imagen)
        {
            necesidad.NecesidadesReferencias = necesidadesReferencias;

            if (necesidad.TipoDonacion == 1)
            {
                necesidad.NecesidadesDonacionesInsumos = necesidadesDonacionesInsumos;
            }
            else
            {
                necesidad.NecesidadesDonacionesMonetarias = necesidadesDonacionesMonetarias;
            }

            necesidad.Foto = GuardarImagen(necesidad.IdUsuarioCreador, imagen);

            _unitOfWork.Necesidades.Add(necesidad);
            _unitOfWork.SaveChanges();
        }

        public void ActualizarNecesidad(
            Necesidades nuevaNecesidad,
            HttpPostedFileBase imagen,
            int idNecesidad)
        {
            var necesidadAnterior = _unitOfWork.Necesidades.Get(idNecesidad);
            var nombreFoto = GuardarImagen(necesidadAnterior.IdUsuarioCreador, imagen);

            necesidadAnterior.Foto = string.IsNullOrEmpty(nombreFoto) ? necesidadAnterior.Foto : nombreFoto;

            if (necesidadAnterior.TipoDonacion == 0)
            {
                for (int i = 0; i < necesidadAnterior.NecesidadesDonacionesMonetarias.Count(); i++)
                {
                    necesidadAnterior.NecesidadesDonacionesMonetarias.ElementAt(i).CBU = nuevaNecesidad.NecesidadesDonacionesMonetarias.ElementAt(i).CBU;
                    necesidadAnterior.NecesidadesDonacionesMonetarias.ElementAt(i).Dinero = nuevaNecesidad.NecesidadesDonacionesMonetarias.ElementAt(i).Dinero;
                }
            }
            else
            {
                for (int i = 0; i < necesidadAnterior.NecesidadesDonacionesInsumos.Count(); i++)
                {
                    necesidadAnterior.NecesidadesDonacionesInsumos.ElementAt(i).Cantidad = nuevaNecesidad.NecesidadesDonacionesInsumos.ElementAt(i).Cantidad;
                    necesidadAnterior.NecesidadesDonacionesInsumos.ElementAt(i).Nombre = nuevaNecesidad.NecesidadesDonacionesInsumos.ElementAt(i).Nombre;
                }
            }            
            
            necesidadAnterior.Descripcion = nuevaNecesidad.Descripcion;
            necesidadAnterior.Denuncias = nuevaNecesidad.Denuncias;
            necesidadAnterior.Nombre = nuevaNecesidad.Nombre;
            necesidadAnterior.FechaFin = nuevaNecesidad.FechaFin;
            necesidadAnterior.TelefonoContacto = nuevaNecesidad.TelefonoContacto;
            necesidadAnterior.NecesidadesReferencias = nuevaNecesidad.NecesidadesReferencias;

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
            if (imagen != null)
            {
                var extension = Path.GetExtension(imagen.FileName);
                var nombreArchivo = $"{Guid.NewGuid().ToString().Substring(0, 10)}{extension}";
                var path = CrearCarpetaSiNoExiste(idUsuario);

                imagen.SaveAs($"{path}\\{nombreArchivo}");

                return nombreArchivo;
            }

            return string.Empty;
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
    }
}
