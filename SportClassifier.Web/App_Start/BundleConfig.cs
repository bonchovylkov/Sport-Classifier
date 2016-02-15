using System.Web;
using System.Web.Optimization;

namespace SportClassifier.Web
{
    public class BundleConfig
    {
         // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
             bundles.Add(new ScriptBundle("~/bundles/Kendo").Include(
                      "~/Scripts/Kendo/kendo.all.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.validate.js",
                        "~/Scripts/jquery.validate.unobtrusive.js",
                       
                        "~/Scripts/jquery.unobtrusive-ajax.js",
                        "~/Scripts/jquery-ui/jquery-ui.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/layout").Include(
                        "~/Scripts/layout/rsvp.min.js",
                        "~/Scripts/layout/app.js",
                        "~/Scripts/layout/filesUpload.js",
                        "~/Scripts/layout/httpRequester.js",
                        "~/Scripts/layout/jquery-ui/utils.js"));


            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/libs").Include(
                       "~/Scripts/toastr/toastr.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap/bootstrap.js",
                      "~/Scripts/bootstrap/respond.js"));


            bundles.Add(new StyleBundle("~/Content/Kendo").Include(
                 "~/Content/Kendo/kendo.common.min.css",
                 "~/Content/Kendo/kendo.default.min.css"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/toastr.css",
                   "~/Content/bootstrap.css",
                   "~/Content/font-awesome.css",
                   "~/Scripts/FileUpload/jquery.fileupload-ui.css",
                   "~/Content/bootstrap-changes.css",
                     //  "~/Content/bootstrap-theme.min.css",
                      "~/Content/site.css"));
        }
    }
}
