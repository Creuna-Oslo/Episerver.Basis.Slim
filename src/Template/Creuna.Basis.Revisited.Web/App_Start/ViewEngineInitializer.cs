using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using System.Linq;
using System.Web.Mvc;

namespace Creuna.Basis.Revisited.Web.App_Start
{
    [InitializableModule]
    public class ViewEngineInitializer : IInitializableModule
    {
        string[] AdditionalViewsSearchPaths = new[] {
            "~/Views/Pages/{1}.cshtml"
        };

        string[] AdditionalPartialViewsSearchPaths = new[] {
            "~/Views/Blocks/{1}.cshtml"
        };

        public void Initialize(InitializationEngine context)
        {
            ViewEngines.Engines.Clear();

            var razorViewEngine = CreateCustomRazorViewEngine();

            ViewEngines.Engines.Add(razorViewEngine);
        }

        RazorViewEngine CreateCustomRazorViewEngine()
        {
            var razorViewEngine = new RazorViewEngine();

            razorViewEngine.ViewLocationFormats = razorViewEngine.ViewLocationFormats
                .Concat(AdditionalViewsSearchPaths)
                .ToArray();

            razorViewEngine.PartialViewLocationFormats = razorViewEngine.PartialViewLocationFormats
                .Concat(AdditionalPartialViewsSearchPaths)
                .ToArray();

            return razorViewEngine;
        }


        public void Uninitialize(InitializationEngine context)
        {}
    }

}