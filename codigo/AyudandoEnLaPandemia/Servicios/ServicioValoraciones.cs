using System.Linq;
using Repositorio;

namespace Servicios
{
    public class ServicioValoraciones
    {
        private readonly UnitOfWork _unitOfWork;

        public ServicioValoraciones(Contexto contexto) 
        {
            _unitOfWork = new UnitOfWork(contexto);
        }

        public bool NecesidadValorada(int idNecesidad, int idUsuario)
        {
            return _unitOfWork.NecesidadesValoraciones.Get(x => x.IdUsuario == idUsuario && x.IdNecesidad == idNecesidad).Any();
        }

        public bool ValorarNecesidad(int idNecesidad, int idUsuario, bool valoracion)
        {
            var valoracionNecesidad = new NecesidadesValoraciones
            {
                IdUsuario = idUsuario,
                IdNecesidad = idNecesidad,
                Valoracion = valoracion
            };

            _unitOfWork.NecesidadesValoraciones.Add(valoracionNecesidad);
            _unitOfWork.SaveChanges();

            var cantidadMeGusta = _unitOfWork.NecesidadesValoraciones.Get(x => x.IdNecesidad == idNecesidad && x.Valoracion).Count();
            var cantidadNoMeGusta = _unitOfWork.NecesidadesValoraciones.Get(x => x.IdNecesidad == idNecesidad && !x.Valoracion).Count();
            var votosTotales = cantidadMeGusta + cantidadNoMeGusta;

            var nuevaValoracion = votosTotales == 0 ? 0 : (int)((cantidadMeGusta * 1.0 / votosTotales) * 100);

            var necesidad = _unitOfWork.Necesidades.Get(idNecesidad);
            necesidad.Valoracion = nuevaValoracion;

            _unitOfWork.SaveChanges();

            return true;
        }
    }
}
