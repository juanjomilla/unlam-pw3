using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using AyudandoEnLaPandemia.App_Start;
using AyudandoEnLaPandemia.AutofacModules;

namespace AyudandoEnLaPandemia
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var builder = new ContainerBuilder();

            // con esto registro todos los controladores en Autofac
            // (tener en cuenta que un controlador es aquella clase que hereda de Controller)
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // le digo a autofac que registre lo que encuentra en los modulos desde el assembly
            // en este caso en particular, va a registrar todo lo que herede de Module desde el assembly DIModule
            builder.RegisterAssemblyModules(typeof(DIModule).Assembly);

            // luego de registrar todo, genero el contenedor
            var container = builder.Build();

            // le digo a MVC cuál va a ser el DI por defecto, en este caso Autofac
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
