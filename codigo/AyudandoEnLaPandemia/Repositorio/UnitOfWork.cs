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
            Usuarios = new UsuarioRepositorio(_dbContext);
        }

        public INecesidadesRepositorio Necesidades { get; }

        public IUsuarioRepositorio Usuarios { get; set; }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
