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
                        "~/Scripts/jquery-3.3.1.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/umd/popper.min.js",
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/respond.js",
                "~/Scripts/summernote-bs4.js"));

            bundles.Add(new ScriptBundle("~/bundles/registrationValidation").Include(
                        "~/Scripts/jquery.validate*", "~/Scripts/ValidationScriptRegistration.js", "~/Scripts/ValidationScriptLogin.js"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/datepickerJS").Include(
                     "~/Scripts/bootstrap-datepicker.min.js"));

            bundles.Add(new StyleBundle("~/bundles/datepickerCSS").Include(
                     "~/Content/bootstrap-datepicker3.min.css",
                     "~/Content/bootstrap-datepicker3.standalone.min"
                     ));

            bundles.Add(new ScriptBundle("~/bundles/selectJS").Include(
                      "~/Scripts/bootstrap-select.min.js"
                      ));

            bundles.Add(new StyleBundle("~/bundles/selectCSS").Include(
                     "~/Content/bootstrap-select.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/loginValidation").Include(
                "~/Scripts/jquery.validate*",
                "~/Scripts/ValidationScriptLogin.js",
                "~/Scripts/ParseURL_login.js"
                ));
        }
    }
}
