using Repositorio.Repositories;

namespace Repositorio
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Contexto _dbContext;

        public UnitOfWork(Contexto dbContext)
        {
            _dbContext = dbContext;
            Necesidades = new NecesidadesRepository(_dbContext);
        }

        public INecesidadesRepository Necesidades { get; }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
