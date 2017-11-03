using System.Web;
using System.Web.Optimization;

namespace Projeto_KB
{
    public class BundleConfig
    {
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
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/Site.css"));
            // new bundle Added for the home view,diferent CSS and Version Bootstrap
            bundles.Add(new StyleBundle("~/Content/Template").Include(
                "~/Content/Template/vendor/bootstrap/css/bootstrap.css",
                "~/Content/Template/css/agency.css"));
            //new bundle Added for Concepts
            bundles.Add(new StyleBundle("~/Content/TemplateAdmin").Include(
                "~/Content/TemplateAdmin/vendor/bootstrap/css/bootstrap.css",
                "~/Content/TemplateAdmin/scss/sb-admin.scss",
                "~/Content/TemplateAdmin/vendor/font-awesome/css/font-awesome.min.css",
                "~/Content/TemplateAdmin/vendor/datatables/dataTables.bootstrap4.css",
                "~/Content/TemplateAdmin/css/sb-admin.css",
                "~/Content/Site.css"));
        }
    }
}
