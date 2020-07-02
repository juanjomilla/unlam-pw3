using System.Collections.Generic;

namespace Repositorio.Repositorios
{
    public class MotivoDenunciaRepositorio : Repositorio<MotivoDenuncia>, IMotivoDenunciaRepositorio
    {
        public MotivoDenunciaRepositorio(Contexto contexto) : base(contexto) { }

        public IEnumerable<MotivoDenuncia> ObtenerTodosMotivosDenuncias()
        {
            return Get();
        }
    }
}