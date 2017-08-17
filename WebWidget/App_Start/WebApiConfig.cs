namespace WebWidget.App_Start
{
    using Microsoft.Practices.Unity;
    using System.Data.Entity;
    using System.Web.Http;
    using WebWidget.DataAccess;
    using WebWidget.DataAccess.Repositories;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var container = new UnityContainer();
            container.RegisterType<IWidgetRepository, WidgetRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<DbContext, WidgetContext>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "WebWidgetApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
