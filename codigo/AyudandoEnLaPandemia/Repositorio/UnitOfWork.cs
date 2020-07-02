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
            NecesidadesValoraciones = new Repositorio<NecesidadesValoraciones>(_dbContext);
            NecesidadesDonacionesMonetarias = new NecesidadesDonacionesMonetariasRepositorio(_dbContext);
            NecesidadesDonacionesInsumos = new NecesidadesDonacionesInsumoRepositorio(_dbContext);
            DenunciasRepo = new DenunciasRepositorio(_dbContext);
            NecesidadesReferencias = new Repositorio<NecesidadesReferencias>(_dbContext);
            MotivosDenuncias = new MotivoDenunciaRepositorio(_dbContext);
        }

        public INecesidadesRepositorio Necesidades { get; }

        public IUsuarioRepositorio Usuarios { get; set; }

        public IDonacionesInsumosRepositorio DonacionesInsumos { get; set; }

        public IDonacionesMonetariasRepositorio DonacionesMonetarias { get; set; }

        public IRepositorio<NecesidadesValoraciones> NecesidadesValoraciones { get; set; }

        public INecesidadesDonacionesMonetariasRepositorio NecesidadesDonacionesMonetarias { get; set; }

        public INecesidadesDonacionesInsumoRepositorio NecesidadesDonacionesInsumos { get; set; }

        public IDenunciasRepositorio DenunciasRepo { get; set; }

        public IRepositorio<NecesidadesReferencias> NecesidadesReferencias { get; set; }
        
        public IMotivoDenunciaRepositorio MotivosDenuncias { get; }

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
