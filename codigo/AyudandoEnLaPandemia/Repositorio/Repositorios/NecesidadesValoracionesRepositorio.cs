namespace Repositorio.Repositorios
{
    public class NecesidadesValoracionesRepositorio : Repositorio<NecesidadesValoraciones>, INecesidadesValoracionesRepositorio
    {
        public NecesidadesValoracionesRepositorio(Contexto contexto) : base(contexto) { }
    }
}
