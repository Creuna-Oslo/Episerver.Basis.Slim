using Creuna.Basis.Revisited.Web.Business.UserHandling;
using Creuna.Basis.Revisited.Web.Search;
using Creuna.Basis.Revisited.Web.Search.EpiserverSearch;
using EPiServer.Logging;
using StructureMap;

namespace Creuna.Basis.Revisited.Web.App_Start
{
    public class WebRegistry : Registry
    {
        public WebRegistry()
        {
            var logger = LogManager.GetLogger();
            logger.Debug($"Initializing {nameof(WebRegistry)}");

            For<ISearchService>().Singleton().Use<EpiserverSearchService>();

            For<IUserAuthenticationHandler>().Singleton().Use<MembershipUserAuthenticationHandler>();
        }
    }
}