using Autofac;
using Repositorio;
using Servicios;

namespace AyudandoEnLaPandemia.AutofacModules
{
    public class DIModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ServicioNecesidad>().AsSelf().InstancePerLifetimeScope();

            builder.RegisterType<ServicioLogin>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<ServicioRegistrar>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<ServicioDonaciones>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<ServicioValoraciones>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<ServicioDenuncias>().AsSelf().InstancePerLifetimeScope();

            builder.RegisterType<Contexto>().AsSelf().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(IRepositorio<>).Assembly).AsClosedTypesOf(typeof(IRepositorio<>));
        }
    }
}