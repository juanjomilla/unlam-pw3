using System.Linq;

namespace Repositorio.Repositorios
{
    public class DenunciasRepositorio : Repositorio<Denuncias>, IDenunciasRepositorio
    {
        public DenunciasRepositorio(Contexto contexto) : base(contexto) { }

        public int ObtenerCantidadDenunciasActivas(int idNecesidad)
        {
            return Get(x => x.IdNecesidad == idNecesidad && x.Estado == 0).Count();
        }
    }
}