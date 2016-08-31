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
                      "~/Scripts/custom.min.js",
                      "~/Scripts/icheck.min.js",
                      "~/Scripts/fastclick.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
                      "~/Scripts/datatables/jquery.dataTables.min.js",
                      "~/Scripts/datatables/dataTables.bootstrap.min.js",
                      "~/Scripts/datatables/dataTables.buttons.min.js",
                      "~/Scripts/datatables/buttons.bootstrap.min.js",
                      "~/Scripts/datatables/buttons.flash.min.js",
                      "~/Scripts/datatables/buttons.html5.min.js",
                      "~/Scripts/datatables/buttons.print.min.js",
                      "~/Scripts/datatables/dataTables.fixedHeader.min.js",
                      "~/Scripts/datatables/dataTables.keyTable.min.js",
                      "~/Scripts/datatables/dataTables.responsive.min.js",
                      "~/Scripts/datatables/responsive.bootstrap.js",
                      "~/Scripts/datatables/datatables.scroller.min.js",
                      "~/Scripts/datatables/jszip.min.js",
                      "~/Scripts/datatables/pdfmake.min.js",
                      "~/Scripts/datatables/vfs_fonts.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/animate.min.css",
                      "~/Content/blue.css",
                      "~/Content/custom.min.css"));

            bundles.Add(new StyleBundle("~/Content/datatables").Include(
                      "~/Content/datatables/buttons.bootstrap.min.css",
                      "~/Content/dataTables.bootstrap.min.css",
                      "~/Content/fixedHeader.bootstrap.min.css",
                      "~/Content/responsive.bootstrap.min.css",
                      "~/Content/scroller.bootstrap.min.css"));


            bundles.Add(new StyleBundle("~/Content/login").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/animate.min.css",
                      "~/Content/custom.min.css"));
        }
    }
}
