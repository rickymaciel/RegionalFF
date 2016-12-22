using System.Web;
using System.Web.Optimization;

namespace RegionalFF
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js",
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
                      "~/Scripts/datatables/vfs_fonts.js",
                      "~/Scripts/datatables/ScriptDatatables.js"));


            bundles.Add(new ScriptBundle("~/bundles/plugins").Include(
                      "~/Scripts/fileinput.min.js",
                      "~/Scripts/jquery.inputmask/dist/jquery.inputmask.bundle.js",
                      "~/Content/bootstrap-fileinput/themes/gly/theme.js",
                      "~/Scripts/Selectize/js/standalone/selectize.min.js",
                      "~/Scripts/icheck.min.js",
                      "~/Scripts/Pnotify/pnotify.js",
                      "~/Scripts/Pnotify/pnotify.buttons.js",
                      "~/Scripts/nprogress.js",
                      "~/Scripts/jquery.mCustomScrollbar.concat.min.js",
                      "~/Scripts/InitPlugins.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/datetimepicker").Include(
                      "~/Scripts/Moment.js",
                      "~/Scripts/moment-with-locales.js",
                      "~/Scripts/bootstrap-datetimepicker.min.js",
                      "~/Scripts/InitDateTime.js"
            ));


            bundles.Add(new ScriptBundle("~/bundles/amcharts").Include(
                      "~/amcharts/amcharts.js",
                      "~/amcharts/serial.js",
                      "~/amcharts/pie.js",
                      "~/amcharts/themes/light.js",
                      "~/amcharts/plugins/export/export.min.js"
            ));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/nprogress.css",
                      "~/Content/animate.min.css",
                      "~/Content/jquery.mCustomScrollbar.min.css",
                      "~/Content/skins/minimal/blue.css",
                      "~/Scripts/Pnotify/pnotify.css",
                      "~/Scripts/Pnotify/pnotify.buttons.css",
                      "~/Content/bootstrap-datetimepicker.css",
                      "~/Content/bootstrap-fileinput/css/fileinput.min.css",
                      "~/Content/bootstrap-theme.min.css",
                      "~/Scripts/Selectize/css/selectize.bootstrap3.css",
                      "~/amcharts/plugins/export/export.css",
                      //"~/Content/docs.min.css",
                      "~/Content/custom.css"
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
