using System.Web.Optimization;

namespace SignalR.WebApi
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery/dist/jquery.js"));

            bundles.Add(new ScriptBundle("~/bundles/signalr").Include(
                        "~/Scripts/signalr/jquery.signalR.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                        "~/Scripts/angular/angular.js",
                        "~/Scripts/angular-uuid/angular-uuid.js"));

            bundles.Add(new ScriptBundle("~/bundles/angularsignalr").Include(
                        "~/Scripts/angular-signalr-hub/signalr-hub.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap/dist/js/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/utils").Include(
                        "~/Scripts/toastr/toastr.js",
                        "~/Scripts/lodash/lodash.js",
                        "~/Scripts/ui_guid_generator/dist/ui-guid-generator.js"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                        "~/App/App.js",
                        "~/App/TodoViewModel.js"));

            bundles.Add(new StyleBundle("~/styles/css").Include(
                      "~/Scripts/bootstrap/dist/css/bootstrap.css",
                      "~/Scripts/toastr/toastr.css",
                      "~/Styles/site.css"));

            BundleTable.EnableOptimizations = true;
        }
    }
}
