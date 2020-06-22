using System.Web.Mvc;
using System.Web.Routing;

namespace AyudandoEnLaPandemia
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Login",
                url: "Login/RegisterConfirm/{IdUsuario}",
                defaults: new { controller = "Login", action = "RegisterConfirm", IdUsuario = UrlParameter.Optional }
            );
        }
    }
}
