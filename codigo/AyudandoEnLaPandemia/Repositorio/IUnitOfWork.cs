namespace Repositorio
{
    public interface IUnitOfWork
    {
        IRepository<Necesidades> NecesidadesRepository { get; }

        void Commit();
    }
}
