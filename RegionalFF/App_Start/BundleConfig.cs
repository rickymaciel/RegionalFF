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
                      "~/Scripts/Selectize/js/standalone/selectize.min.js",
                      "~/Scripts/icheck.min.js",
                      "~/Scripts/Pnotify/pnotify.js",
                      "~/Scripts/Pnotify/pnotify.buttons.js",
                      "~/Scripts/nprogress.js",
                      "~/Scripts/custom.min.js",
                      "~/Scripts/respond.min.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/datatable").Include(
                      "~/Scripts/datatables/jquery.dataTables.min.js",
                      "~/Scripts/datatables/dataTables.responsive.min.js",
                      "~/Scripts/datatables/dataTables.bootstrap4.min.js",
                      "~/Scripts/datatables/dataTables.buttons.min.js",
                      "~/Scripts/datatables/buttons.bootstrap.min.js",
                      "~/Scripts/datatables/buttons.flash.min.js",
                      "~/Scripts/datatables/buttons.html5.min.js",
                      "~/Scripts/datatables/buttons.print.min.js",
                      "~/Scripts/datatables/jszip.min.js",
                      "~/Scripts/datatables/pdfmake.min.js",
                      "~/Scripts/datatables/ScriptDatatables.js",
                      "~/Scripts/datatables/vfs_fonts.js"));


            bundles.Add(new ScriptBundle("~/bundles/plugins").Include(
                      "~/Scripts/fastclick.js",
                      "~/Scripts/jquery.mCustomScrollbar.concat.min.js",
                      "~/Scripts/Moment.js",
                      "~/Scripts/moment-with-locales.js",
                      "~/Scripts/bootstrap-datetimepicker.min.js",
                      "~/Scripts/plugins/purify.min.js",
                      "~/Scripts/plugins/sortable.min.js",
                      "~/Scripts/plugins/canvas-to-blob.min.js",
                      "~/Scripts/fileinput.min.js",
                      "~/Scripts/jquery.inputmask/dist/jquery.inputmask.bundle.js",
                      "~/Content/bootstrap-fileinput/themes/gly/theme.js"
            ));


            bundles.Add(new ScriptBundle("~/bundles/echarts").Include(
                      "~/Scripts/echarts/dist/echarts.min.js",
                      "~/Scripts/echarts/map/js/world.js",
                      "~/Scripts/pieconfig.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/amcharts").Include(
                      "~/Scripts/amcharts/amcharts.js",
                      "~/Scripts/amcharts/serial.js",
                      "~/Scripts/amcharts/pie.js",
                      "~/Scripts/amcharts/themes/light.js",
                      "~/Scripts/amcharts/plugins/export/libs/fabric.js/fabric.min.js",
                      "~/Scripts/amcharts/plugins/export/libs/FileSaver.js/FileSaver.min.js",
                      "~/Scripts/amcharts/plugins/export/libs/jszip/jszip.min.js",
                      "~/Scripts/amcharts/plugins/export/libs/pdfmake/pdfmake.min.js",
                      "~/Scripts/amcharts/plugins/export/libs/xlsx/xlsx.min.js",
                      "~/Scripts/amcharts/plugins/export/libs/xlsx/vfs_fonts.js",
                      "~/Scripts/amcharts/plugins/export/export.js"
            ));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/font-awesome.min.css",
                      "~/Content/animate.min.css",
                      "~/Content/jquery.mCustomScrollbar.min.css",
                      "~/Content/skins/minimal/blue.css",
                      "~/Scripts/Pnotify/pnotify.css",
                      "~/Scripts/Pnotify/pnotify.buttons.css",
                      "~/Content/bootstrap-datetimepicker.css",
                      "~/Content/ionicons.min.css",
                      "~/Content/bootstrap-fileinput/css/fileinput.min.css",
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap-theme.min.css",
                      "~/Content/nprogress.css",
                      "~/Content/custom.css",
                      "~/Content/docs.min.css",
                      "~/Scripts/Selectize/css/selectize.bootstrap3.css",
                      "~/Scripts/amcharts/plugins/export/export.css"
                      ));


            bundles.Add(new StyleBundle("~/Content/datatable").Include(
                      "~/Content/datatables/dataTables.bootstrap4.min.css",
                      "~/Content/datatables/buttons.bootstrap.min.css",
                      "~/Content/datatables/responsive.bootstrap.min.css"));


            bundles.Add(new StyleBundle("~/Content/login").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/skins/minimal/blue.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/animate.min.css",
                      "~/Scripts/Pnotify/pnotify.css",
                      "~/Scripts/Pnotify/pnotify.buttons.css",
                      "~/Content/custom.css",
                      "~/Content/docs.min.css"));
        }
    }
}
