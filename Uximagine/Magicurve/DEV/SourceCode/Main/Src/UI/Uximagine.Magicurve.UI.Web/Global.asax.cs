using Newtonsoft.Json;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.IO;
using Uximagine.Magicurve.UI.Web.Common;

namespace Uximagine.Magicurve.UI.Web
{
    /// <summary>
    /// The web application.
    /// </summary>
    public class WebApiApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Application_s the start.
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            log4net.Config.XmlConfigurator.Configure(new FileInfo(Server.MapPath("~/Web.config")));

            HttpConfiguration config = GlobalConfiguration.Configuration;

            config.Formatters.JsonFormatter.SerializerSettings.Formatting =
                Formatting.Indented;

            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(
                new Newtonsoft.Json.Converters.StringEnumConverter());

            Startup.Init();
        }

        /// <summary>
        /// Handles the Error event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = HttpContext.Current.Server.GetLastError();

            Exception actualException = null;

            if (ex != null)
            {
                if (ex.InnerException != null)
                {
                    actualException = ex.InnerException;
                }
                else
                {
                    actualException = ex;
                }
            }

            string errorDetails = JsonConvert.SerializeObject(actualException);

            HttpContext.Current.Response.Write(errorDetails);
        }
    }
}
