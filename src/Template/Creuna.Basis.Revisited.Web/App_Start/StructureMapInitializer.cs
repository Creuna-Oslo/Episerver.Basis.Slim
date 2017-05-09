using EPiServer.Framework;
using EPiServer.ServiceLocation;
using EPiServer.Framework.Initialization;
using System.Web.Mvc;
using StructureMap.Graph;
using System.Web.Http;
using Creuna.Basis.Revisited.Web.App_Start.Structuremap;
using StructureMap;

namespace Creuna.Basis.Revisited.Web.App_Start
{
    [ModuleDependency(typeof(ServiceContainerInitialization))]
    [InitializableModule]
    public class StructureMapInitializer : IConfigurableModule
    {
        const string SolutionPrefix = "Creuna.Basis.Revisited.Web";

        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            var container = context.StructureMap();

            container.Configure(ConfigureContainer);

            DependencyResolver.SetResolver(new StructureMapDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new StructureMapDependencyResolver(container);
        }

        void ConfigureContainer(ConfigurationExpression container)
            => container.Scan(ScanForRegistries);

        void ScanForRegistries(IAssemblyScanner scanner)
        {
            scanner.AssembliesFromApplicationBaseDirectory(f => f.FullName.StartsWith(SolutionPrefix));
            scanner.WithDefaultConventions();
            scanner.LookForRegistries();
        }

        public void Initialize(InitializationEngine context) {}
        public void Uninitialize(InitializationEngine context) {}
    }
}