using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using RestImageResize.EPiServer;

namespace Creuna.Basis.Revisited.Web.App_Start
{
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    [InitializableModule]
    public class ImageResizeInitializer : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
            => OpenWaves.ServiceLocator.SetResolver(new OpenWaves.BasicResolver().RegisterRestImageResize());

        public void Uninitialize(InitializationEngine context) { }
    }
}