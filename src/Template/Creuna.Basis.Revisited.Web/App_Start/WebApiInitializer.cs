using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace Creuna.Basis.Revisited.Web.App_Start
{
    [InitializableModule]
    public class WebApiInitializer : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
            => GlobalConfiguration.Configure(Configure);

        public void Uninitialize(InitializationEngine context) { }

        void Configure(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            SetCamelCaseResolverAsDefault(config);
        }

        void SetCamelCaseResolverAsDefault(HttpConfiguration config)
        {
            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}