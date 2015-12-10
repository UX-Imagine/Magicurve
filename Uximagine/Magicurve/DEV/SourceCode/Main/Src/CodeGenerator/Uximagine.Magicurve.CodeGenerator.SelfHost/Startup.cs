namespace Uximagine.Magicurve.CodeGenerator.SelfHost
{
    using System.Web.Http;
    using System;
    using Owin;

    /// <summary>
    /// The startup.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// The value controller type.
        /// Hack for other assembly types.
        /// </summary>
        Type valueControllerType = typeof(Controllers.CodeController);

        /// <summary>
        /// The configuration.
        /// </summary>
        /// <param name="appBuilder">
        /// The app builder.
        /// </param>
        public void Configuration(IAppBuilder appBuilder)
        {
            HttpConfiguration config = new HttpConfiguration();

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional });

            appBuilder.UseWebApi(config);
        }
    }
}
