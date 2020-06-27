using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using Repositorio;
using Repositorio.Repositorios;

namespace Servicios
{
    public class ServicioDonaciones : IDisposable
    {
        private readonly UnitOfWork _unitOfWork;

        private IDonacionesMonetariasRepositorio _donacionesMonetariasRepositorio;

        private INecesidadesDonacionesMonetariasRepositorio _necesidadesDonacionesMonetariasRepositorio;

        public ServicioDonaciones(Contexto contexto, IDonacionesMonetariasRepositorio donacionesMonetariasRepositorio, INecesidadesDonacionesMonetariasRepositorio necesidadesDonacionesMonetariasRepositorio)
        {
            _unitOfWork = new UnitOfWork(contexto);
            _donacionesMonetariasRepositorio = donacionesMonetariasRepositorio;
            _necesidadesDonacionesMonetariasRepositorio = necesidadesDonacionesMonetariasRepositorio;
        }

        public IEnumerable<DonacionesInsumos> GetDonacionesInsumosUsuario(int idUsuario)
        {
            return _unitOfWork.DonacionesInsumos.Get(x => x.IdUsuario == idUsuario);
        }

        public NecesidadesDonacionesMonetarias GetNecesidadesDonacionesMonetarias(int idNecesidad)
        {
            return _unitOfWork.NecesidadesDonacionesMonetarias.BuscarNecesidad(idNecesidad);
        }

        public IEnumerable<DonacionesMonetarias> GetDonacionesMonetariasUsuario(int idUsuario)
        {
            return _unitOfWork.DonacionesMonetarias.Get(x => x.IdUsuario == idUsuario);
        }

        public decimal GetTotalDonacionesInsumo(int idNecesidadDonacionInsumo)
        {
            return _unitOfWork.DonacionesInsumos.GetTotalDonaciones(idNecesidadDonacionInsumo);
        }

        public decimal GetTotalDonacionesMonetaria(int idNecesidadDonacionMonetaria)
        {
            return _unitOfWork.DonacionesMonetarias.GetTotalDonaciones(idNecesidadDonacionMonetaria);
        }


        public void Dispose()
        {
            if (_unitOfWork != null)
            {
                _unitOfWork.Dispose();
            }
        }

        public void CrearDonacionMonetaria(DonacionesMonetarias donacion)
        {
            _donacionesMonetariasRepositorio.CrearDonacionMonetaria(donacion);
        }

        public string GuardarAdjunto(int idUsuario, HttpPostedFileBase archivo)
        {   
            var extension = Path.GetExtension(archivo.FileName);
            var nombreArchivo = $"{Guid.NewGuid().ToString().Substring(0, 10)}{extension}";
            var path = CrearCarpetaSiNoExiste(idUsuario);

            archivo.SaveAs($"{path}\\{nombreArchivo}");

            return nombreArchivo;
        }

        private object CrearCarpetaSiNoExiste(int idUsuario)
        {
            var path = HttpContext.Current.Server.MapPath($"~/Content/donaciones/comprobantes/{idUsuario}");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }
    }
}
