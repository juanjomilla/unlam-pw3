namespace Repositorio.Repositorios
{
    public interface IDenunciasRepositorio : IRepositorio<Denuncias>
    {
        int ObtenerCantidadDenunciasActivas(int idNecesidad);
    }
}
