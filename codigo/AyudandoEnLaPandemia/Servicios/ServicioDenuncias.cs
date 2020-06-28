using System.Collections.Generic;
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

        public IEnumerable<Denuncias> ObtenerDenunciasActivas()
        {
            return _unitOfWork.DenunciasRepo.Get(x => x.Estado == 0);
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
                denuncia.Estado = 2;

                _unitOfWork.SaveChanges();
            }
        }
    }
}
