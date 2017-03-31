using EPiServer.Framework;
using EPiServer.Framework.Initialization;

namespace Creuna.Basis.Revisited.Web.App_Start
{
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    [InitializableModule]
    public class ImageResizerInitializer : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            
        }

        public void Uninitialize(InitializationEngine context)
        {}
    }
}