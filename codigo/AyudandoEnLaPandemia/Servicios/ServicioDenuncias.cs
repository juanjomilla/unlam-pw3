using System;
using System.Collections.Generic;
using System.Linq;
using Repositorio;

namespace Servicios
{
    public class ServicioDenuncias
    {
        private readonly UnitOfWork _unitOfWork;

        public ServicioDenuncias(Contexto contexto)
        {
            _unitOfWork = new UnitOfWork(contexto);
        }

        public IEnumerable<MotivoDenuncia> ObtenerMotivosDenuncias()
        {
            return _unitOfWork.MotivosDenuncias.ObtenerTodosMotivosDenuncias();
        }

        public MotivoDenuncia ObtenerMotivoDenuncia(int idMotivoDenuncia)
        {
            return _unitOfWork.MotivosDenuncias.Get(idMotivoDenuncia);
        }

        public void CrearDenuncia(Denuncias denuncia)
        {
            var cantidadDenunciasActivas = _unitOfWork.DenunciasRepo.ObtenerCantidadDenunciasActivas(denuncia.Necesidades.IdNecesidad);

            if (cantidadDenunciasActivas >= 4)
            {
                denuncia.Necesidades.Estado = 4;
            }

            _unitOfWork.DenunciasRepo.Add(denuncia);
            _unitOfWork.SaveChanges();
        }

        public IEnumerable<Denuncias> ObtenerDenunciasActivas()
        {
            return _unitOfWork.DenunciasRepo.Get(x => x.Estado == 0);
        }

        public bool NecesidadDenunciada(Necesidades necesidad, Usuarios usuario)
        {
            return _unitOfWork.DenunciasRepo.Get(x => x.IdUsuario == usuario.IdUsuario && x.IdNecesidad == necesidad.IdNecesidad).Any();
        }

        public void AceptarDenuncia(int idDenuncia)
        {
            var denuncia = _unitOfWork.DenunciasRepo.Get(idDenuncia);

            if (denuncia != null)
            {
                denuncia.Estado = 1;
                denuncia.Necesidades.Estado = 3;

                _unitOfWork.SaveChanges();
            }
        }

        public void DesestimarDenuncia(int idDenuncia)
        {
            var denuncia = _unitOfWork.DenunciasRepo.Get(idDenuncia);

            if (denuncia != null)
            {
                var cantidadDenunciasActivas = _unitOfWork.DenunciasRepo.ObtenerCantidadDenunciasActivas(denuncia.Necesidades.IdNecesidad);
                if (cantidadDenunciasActivas <= 5)
                {
                    denuncia.Necesidades.Estado = 0;
                }

                denuncia.Estado = 2;

                _unitOfWork.SaveChanges();
            }
        }
    }
}
