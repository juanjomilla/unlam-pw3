using Repositorio.Repositorios;

namespace Repositorio
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Contexto _dbContext;

        public UnitOfWork(Contexto dbContext)
        {
            _dbContext = dbContext;
            Necesidades = new NecesidadesRepositorio(_dbContext);
        }

        public INecesidadesRepositorio Necesidades { get; }

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
