using System.Web;
using System.Web.Optimization;

namespace BusinessTripApplication
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/popper.js",
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/summernote-bs4.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/datepickerJS").Include(
                     "~/Scripts/bootstrap-datepicker.js"));

            bundles.Add(new StyleBundle("~/bundles/datepickerCSS").Include(
                     "~/Content/bootstrap-datepicker.css"));

            bundles.Add(new ScriptBundle("~/bundles/selectJS").Include(
                      "~/Scripts/bootstrap-select.js",
                      "~/Scripts/script-bootstrap-select.js",
                      "~/Scripts/bootstrap-select.min.js"
                      ));

            bundles.Add(new StyleBundle("~/bundles/selectCSS").Include(
                     "~/Content/bootstrap-select.css",
                     "~/Content/bootstrap-select.min.css"));
        }
    }
}
