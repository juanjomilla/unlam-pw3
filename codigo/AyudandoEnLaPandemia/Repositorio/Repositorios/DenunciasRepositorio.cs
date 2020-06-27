namespace Repositorio.Repositorios
{
    public class DenunciasRepositorio : Repositorio<Denuncias>, IDenunciasRepositorio
    {
        public DenunciasRepositorio(Contexto contexto) : base(contexto) { }
    }
}
