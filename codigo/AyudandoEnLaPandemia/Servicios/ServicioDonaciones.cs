using System;
using System.Collections.Generic;
using Repositorio;
using Repositorio.Repositorios;

namespace Servicios
{
    public class ServicioDonaciones : IDisposable
    {
        private readonly UnitOfWork _unitOfWork;

        private IDonacionesMonetariasRepositorio _donacionesMonetariasRepositorio;

        public ServicioDonaciones(Contexto contexto, IDonacionesMonetariasRepositorio donacionesMonetariasRepositorio)
        {
            _unitOfWork = new UnitOfWork(contexto);
            _donacionesMonetariasRepositorio = donacionesMonetariasRepositorio;
        }

        public IEnumerable<DonacionesInsumos> GetDonacionesInsumosUsuario(int idUsuario)
        {
            return _unitOfWork.DonacionesInsumos.Get(x => x.IdUsuario == idUsuario);
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

        //public int BuscarIdNecesidadDonacionMonetaria(int idNecesidad)
        //{
        //   return IdNecesidadDonacionMonetaria = _donacionesMonetariasRepositorio.BuscarIdNecesidadDonacionMonetaria(idNecesidad);
        //}

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
    }
}
