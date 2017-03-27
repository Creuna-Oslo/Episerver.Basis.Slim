using EPiServer.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using EPiServer.Framework.Initialization;
using EPiServer.DataAbstraction;
using Creuna.Basis.Revisited.Web.Business;
using EPiServer.Security;

namespace Creuna.Basis.Revisited.Web.App_Start
{
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    [InitializableModule]
    public class TabInitializer : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            var tabRepository = context.Locate.Advanced.GetInstance<ITabDefinitionRepository>();

            foreach (var tab in GetTabs())
                RegisterTab(tabRepository, tab);
        }

        public void Uninitialize(InitializationEngine context) { }

        IEnumerable<TabDefinition> GetTabs()
        {

            yield return new TabDefinition
            {
                Name = ApplicationConstants.TabNames.Seo,
                RequiredAccess = AccessLevel.Edit,
                SortIndex = 1000
            };

        }

        void RegisterTab(ITabDefinitionRepository repository, TabDefinition tab)
        {
            var existingTab = repository
                .List()
                .FirstOrDefault(x => string.Compare(x.Name, tab.Name, StringComparison.InvariantCultureIgnoreCase) == 0);

            if (existingTab != null)
                tab.ID = existingTab.ID;

            repository.Save(tab);
        }
    }
}