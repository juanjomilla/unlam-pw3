using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AyudandoEnLaPandemia.HttpModules
{
    public class UsuarioAutenticado : IHttpModule
    {
        private readonly IEnumerable<string> PathAnonimos = new List<string>
        {
            "/",
            "/Login/LoginUsuario",
            "/Login/RegistroUsuario",
            "/Login/RegisterConfirm",
            "/Login/Confirm"
        };

        private readonly string ScriptsPath = "/Scripts/";
        private readonly string BundlesPath = "/Bundles/";
        private readonly string ContentPath = "/Content/";
        private readonly string ErrorPath = "/Error/";

        public void Dispose()
        {

        }

        public void Init(HttpApplication context)
        {
            context.PreRequestHandlerExecute += new EventHandler(EstaAutenticado);
        }

        private void EstaAutenticado(object sender, EventArgs e)
        {
            var app = (HttpApplication)sender;
            var context = app.Context;

            if (!EstaAutenticado(context))
            {
                if (!EsAnonimo(context))
                {
                    var pathARedirigir = context.Request.Path;
                    context.Response.Redirect($"/Login/LoginUsuario?redirigir={pathARedirigir}");
                }
            }
        }

        private bool EsAnonimo(HttpContext context)
        {
            return EsPathAnonimo(context) || EsScriptPath(context) || EsContentPath(context) || EsBundlesPath(context) || EsErrorPath(context);
        }

        private bool EstaAutenticado(HttpContext context)
        {
            return context.Session?["UsuarioID"] != null;
        }

        private bool EsPathAnonimo(HttpContext context)
        {
            return PathAnonimos.Any(x => context.Request.Path.Equals(x));
        }

        private bool EsScriptPath(HttpContext context)
        {
            return context.Request.Path.StartsWith(ScriptsPath);
        }

        private bool EsErrorPath(HttpContext context)
        {
            return context.Request.Path.StartsWith(ErrorPath);
        }

        private bool EsContentPath(HttpContext context)
        {
            return context.Request.Path.StartsWith(ContentPath);
        }

        private bool EsBundlesPath(HttpContext context)
        {
            return context.Request.Path.StartsWith(BundlesPath);
        }
    }
}