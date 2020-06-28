using System.Web.Optimization;

namespace AyudandoEnLaPandemia.App_Start
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Bundles/jquery").Include("~/Scripts/jquery-3.5.1.*"));

            bundles.Add(new ScriptBundle("~/Bundles/modernizr").Include("~/Scripts/modernizr-2.8.3.*"));

            bundles.Add(new ScriptBundle("~/Bundles/bootstrap").Include("~/Scripts/bootstrap.*"));

            bundles.Add(new ScriptBundle("~/Bundles/scriptsProyecto").Include("~/Scripts/file-upload-perfil.*", "~/Scripts/necesidad/crearNecesidad.*"));

            bundles.Add(new StyleBundle("~/Content/styles").Include("~/Content/Site.*", "~/Content/bootstrap.*"));

            BundleTable.EnableOptimizations = true;
        }
    }
}