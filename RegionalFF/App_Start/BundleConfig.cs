using System.Web;
using System.Web.Optimization;

namespace RegionalFF
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
                      "~/Scripts/bootstrap.min.js",
                      "~/Content/Selectize/js/standalone/selectize.js",
                      "~/Scripts/icheck.min.js",
                      "~/Scripts/fastclick.js",
                      "~/Scripts/nprogress.js",
                      "~/Scripts/jquery.mCustomScrollbar.concat.min.js",
                      "~/Scripts/pnotify.js",
                      "~/Scripts/pnotify.buttons.js",
                      "~/Scripts/Moment.js",
                      "~/Scripts/moment-with-locales.js",
                      "~/Scripts/bootstrap-datetimepicker.min.js",
                      "~/Scripts/fileinput.min.js",
                      "~/Scripts/custom.min.js",
                      "~/Content/bootstrap-fileinput/themes/fa/theme.js",
                      "~/Content/bootstrap-fileinput/themes/gly/theme.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
                      "~/Scripts/datatables/jquery.dataTables.min.js",
                      "~/Scripts/datatables/dataTables.bootstrap4.min.js",
                      "~/Scripts/datatables/dataTables.buttons.min.js",
                      "~/Scripts/datatables/buttons.bootstrap.min.js",
                      "~/Scripts/datatables/buttons.flash.min.js",
                      "~/Scripts/datatables/buttons.html5.min.js",
                      "~/Scripts/datatables/buttons.print.min.js",
                      "~/Scripts/datatables/jszip.min.js",
                      "~/Scripts/datatables/pdfmake.min.js",
                      "~/Scripts/datatables/vfs_fonts.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap-theme.min.css",
                      "~/Content/Selectize/css/selectize.bootstrap3.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/animate.min.css",
                      "~/Content/jquery.mCustomScrollbar.min.css",
                      "~/Content/blue.css",
                      "~/Content/pnotify.css",
                      "~/Content/pnotify.buttons.css",
                      "~/Content/bootstrap-datetimepicker.css",
                      "~/Content/ionicons.min.css",
                      "~/Content/bootstrap-fileinput/css/fileinput.css",
                      "~/Content/custom.min.css"));

            bundles.Add(new StyleBundle("~/Content/datatables").Include(
                      "~/Content/datatables/buttons.bootstrap.min.css",
                      "~/Content/datatables/dataTables.bootstrap4.min.css"));


            bundles.Add(new StyleBundle("~/Content/login").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/blue.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/animate.min.css",
                      "~/Content/pnotify.css",
                      "~/Content/pnotify.buttons.css",
                      "~/Content/custom.min.css"));
        }
    }
}
