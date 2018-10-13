using System.Web.Http;

namespace Example.WebApi
{
    public static class WebApiConfig
    {
        private const string DefaultRouteName = "DefaultRoute";

        public static void Register(HttpConfiguration configuration)
        {
            configuration.Routes.MapHttpRoute(
                name: DefaultRouteName,
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            configuration.EnableSystemDiagnosticsTracing();
        }
    }
}