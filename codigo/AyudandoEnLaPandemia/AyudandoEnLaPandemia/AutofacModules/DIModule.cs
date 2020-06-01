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

            builder.RegisterType<Contexto>().AsSelf().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(IRepository<>).Assembly).AsClosedTypesOf(typeof(IRepository<>));
        }
    }
}