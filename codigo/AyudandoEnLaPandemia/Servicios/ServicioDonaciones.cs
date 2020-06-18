using System;
using System.Collections.Generic;
using Repositorio;

namespace Servicios
{
    public class ServicioDonaciones : IDisposable
    {
        private readonly UnitOfWork _unitOfWork;

        public ServicioDonaciones(Contexto contexto)
        {
            _unitOfWork = new UnitOfWork(contexto);
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

        public void Dispose()
        {
            if (_unitOfWork != null)
            {
                _unitOfWork.Dispose();
            }
        }
    }
}
