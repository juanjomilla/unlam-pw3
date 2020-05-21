using Autofac;
using Dao;
using Servicios;

namespace AyudandoEnLaPandemia.AutofacModules
{
    public class DIModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ServicioNecesidad>().AsSelf().InstancePerLifetimeScope();

            builder.RegisterType<NecesidadDao>().As<INecesidadDao>().InstancePerLifetimeScope();

            builder.RegisterType<ServicioLogin>().AsSelf().InstancePerLifetimeScope();
        }
    }
}