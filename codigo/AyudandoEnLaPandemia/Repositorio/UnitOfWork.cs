namespace Repositorio
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Contexto _dbContext;

        private static IRepository<Necesidades> _necesidadesRepository;

        public UnitOfWork()
        {
            _dbContext = new Contexto();
        }

        public IRepository<Necesidades> NecesidadesRepository => _necesidadesRepository ?? (_necesidadesRepository = new Repository<Necesidades>());

        public void Commit()
        {
            _dbContext.SaveChanges();
        }
    }
}
