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
            DonacionesInsumos = new DonacionesInsumosRepositorio(_dbContext);
            DonacionesMonetarias = new DonacionesMonetariasRepositorio(_dbContext);
            NecesidadesValoraciones = new NecesidadesValoracionesRepositorio(_dbContext);
            DenunciasRepo = new DenunciasRepositorio(_dbContext);
        }

        public INecesidadesRepositorio Necesidades { get; }

        public IUsuarioRepositorio Usuarios { get; set; }

        public IDonacionesInsumosRepositorio DonacionesInsumos { get; set; }

        public IDonacionesMonetariasRepositorio DonacionesMonetarias { get; set; }

        public INecesidadesValoracionesRepositorio NecesidadesValoraciones { get; set; }

        public IDenunciasRepositorio DenunciasRepo { get; set; }

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
