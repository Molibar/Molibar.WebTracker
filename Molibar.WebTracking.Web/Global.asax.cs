using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Molibar.Infrastructure.IoC.StructureMap;
using Molibar.Infrastructure.Logging;

namespace Molibar.WebTracking.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }

        public void InitializeContainer()
        {
            var container = IoC.IoC.Initialize();
            DependencyResolver.SetResolver(new StructureMapDependencyResolver(container));

            var oldProvider = FilterProviders.Providers.Single(f => f is FilterAttributeFilterProvider);
            FilterProviders.Providers.Remove(oldProvider);
            FilterProviders.Providers.Add(new InjectableFilterProvider(container));
        }

        protected void Application_Start()
        {
            InitializeContainer();
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();
            Response.Clear();
            if (!exception.Message.Contains("/favicon.ico")) Log.FatalMessage(GetType(), "Uncaught Exception", exception);
        }
    }
}