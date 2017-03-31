using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using ImageResizer.Plugins.EPiServerBlobReader;
using ImageResizer.Configuration;

namespace Creuna.Basis.Revisited.Web.App_Start
{
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    [InitializableModule]
    public class ImageResizerInitializer : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            new EPiServerBlobReaderPlugin().Install(Config.Current);
        }

        public void Uninitialize(InitializationEngine context)
        {}
    }
}