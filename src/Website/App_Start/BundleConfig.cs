using System.Web;
using System.Web.Optimization;

namespace WebApplication2
{
    public class BundleConfig
    {
        private const string TelerikPathContent = "~/Content/kendo/2016.2.504";
        private const string TelerikPathScript = "~/scripts/kendo/2016.2.504";
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                         "~/scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/scripts/bootstrap-datepicker.js",
            //          "~/scripts/locales/bootstrap-datepicker.fr.min.js",
            //          "~/scripts/bootstrap.js",
            //          "~/scripts/bootstrap-switch.min.js"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                      "~/Content/bootstrap-datepicker.min.css",
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap-switch.min.css",
                      "~/Content/VinciWidget.css",
                      "~/Content/Site.css"));

            bundles.Add(new StyleBundle("~/bundles/TelerikCSS").Include(
                     string.Format("{0}/kendo.common-material.min.css", TelerikPathContent),
                     string.Format("{0}/kendo.dataviz.min.css", TelerikPathContent),
                     string.Format("{0}/kendo.material.min.css", TelerikPathContent),
                     string.Format("{0}/kendo.dataviz.material.min.css", TelerikPathContent)
                     ));

            //bundles.Add(new ScriptBundle("~/bundles/TelerikJS").Include(
            //          string.Format("{0}/jszip.min.js", TelerikPathScript),
            //          string.Format("{0}/kendo.all.min.js", TelerikPathScript),
            //          string.Format("{0}/kendo.aspnetmvc.min.js", TelerikPathScript),
            //          string.Format("{0}/cultures/kendo.culture.fr-FR.min.js", TelerikPathScript),
            //          string.Format("{0}/messages/kendo.messages.fr-FR.min.js", TelerikPathScript),
            //         "~/scripts/kendo.modernizr.custom.js"));
        }
    }
}
