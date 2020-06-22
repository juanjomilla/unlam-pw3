using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using HistorialDonacionesWebService.AutofacModules;

namespace HistorialDonacionesWebService
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            var config = GlobalConfiguration.Configuration;
            var builder = new ContainerBuilder();

            // con esto registro todos los controladores en Autofac
            // (tener en cuenta que un controlador es aquella clase que hereda de ApiController)
            builder.RegisterApiControllers(typeof(WebApiApplication).Assembly);

            // le digo a autofac que registre lo que encuentra en los modulos desde el assembly
            // en este caso en particular, va a registrar todo lo que herede de Module desde el assembly DIModule
            builder.RegisterAssemblyModules(typeof(DIModule).Assembly);

            // luego de registrar todo, genero el contenedor
            var container = builder.Build();

            // le digo a Web API cuál va a ser el DI por defecto, en este caso Autofac
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
