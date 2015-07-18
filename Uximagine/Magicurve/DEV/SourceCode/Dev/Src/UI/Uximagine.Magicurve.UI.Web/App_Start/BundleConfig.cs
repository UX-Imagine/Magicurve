using System.Web.Optimization;

namespace Uximagine.Magicurve.UI.Web
{
    /// <summary>
    /// The bundle configuration file.
    /// </summary>
    public class BundleConfig
    {
        //// For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862

        /// <summary>
        /// Registers the bundles.
        /// </summary>
        /// <param name="bundles">
        /// The bundles.
        /// </param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            //// Use the development version of Modernizer to develop with and learn from. Then, when you're
            //// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                "~/Scripts/fx/angular/angular.js",
                "~/Scripts/fx/angular/angular-route.js",
                "~/Scripts/fx/angular-ui/ui-bootstrap.js",
                "~/Scripts/fx/angular/angular-ui-router.js",
                "~/Scripts/fx/angular/angular-animate.js",
                "~/Scripts/toaster.js"));

            bundles.Add(new ScriptBundle("~/bundles/magicurveApp").Include(
                "~/Scripts/app/views/home/magicurve.app.js",
                "~/Scripts/app/views/home/magicurve.app.controller.home.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/toaster.css",
                      "~/Content/site.css"));

            bundles.Add(new LessBundle("~/Content/less").Include("~/Content/*.less"));

            //// Set EnableOptimizations to false for debugging. For more information,
            //// visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = false;
        }
    }
}
