using System.Web.Optimization;

namespace Datalist.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterScripts(bundles);
            RegisterStyles(bundles);
        }
        private static void RegisterScripts(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/Page/Bundle")
                .Include("~/Scripts/JQuery/*.js")
                .Include("~/Scripts/JQueryUI/*.js")
                .Include("~/Scripts/MvcDatalist/*.js")
                .Include("~/Scripts/Shared/*.js"));
        }
        private static void RegisterStyles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/Page/Bundle")
                .Include("~/Content/JQueryUI/*.css")
                .Include("~/Content/Bootstrap/*.css")
                .Include("~/Content/FontAwesome/*.css")
                .Include("~/Content/MvcDatalist/*.css")
                .Include("~/Content/Shared/*.css"));
        }
    }
}