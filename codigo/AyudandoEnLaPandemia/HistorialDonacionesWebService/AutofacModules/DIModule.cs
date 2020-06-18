using Autofac;
using Repositorio;
using Servicios;

namespace HistorialDonacionesWebService.AutofacModules
{
    public class DIModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ServicioDonaciones>().AsSelf().InstancePerLifetimeScope();

            builder.RegisterType<Contexto>().AsSelf().InstancePerLifetimeScope();
        }
    }
}