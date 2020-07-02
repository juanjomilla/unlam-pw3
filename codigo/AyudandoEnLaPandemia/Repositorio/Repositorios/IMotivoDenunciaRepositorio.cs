using System.Collections.Generic;

namespace Repositorio.Repositorios
{
    public interface IMotivoDenunciaRepositorio : IRepositorio<MotivoDenuncia>
    {
        IEnumerable<MotivoDenuncia> ObtenerTodosMotivosDenuncias();
    }
}