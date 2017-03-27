using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using System.Web.Routing;
using System.Web.Mvc;
using Creuna.Basis.Revisited.Web.Business;

namespace Creuna.Basis.Revisited.Web.App_Start
{
    [ModuleDependency(typeof(WebApiInitializer))]
    [InitializableModule]
    public class MvcRoutesInitializer : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            RegisterRoutes(RouteTable.Routes);
        }

        public void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                name: ApplicationConstants.CustomRoutes.NotFound,
                url: "error/404",
                defaults: new { controller = "ErrorPages", action = "NotFound" }
            );
        }

        public void Uninitialize(InitializationEngine context) {}
    }
}