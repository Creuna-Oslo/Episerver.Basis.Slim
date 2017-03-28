using EPiServer.Globalization;
using EPiServer.Web.Routing;
using EPiServer.Web.Routing.Segments;
using System.Linq;
using System.Web.Mvc;

namespace Creuna.Basis.Revisited.Web.Controllers.Base
{
    /// <summary>
    /// Used by pages not connected to an episerver page that should be localized.
    /// The page needs some way to determine the language from an url, e.g. because of a rewritten request (error page), 
    /// or returnUrl-parameter (401-challenges).
    /// </summary>
    public class LocalizedNonContentControllerBase : Controller
    {
        protected ILanguageSegmentMatcher LanguageSegmentMatcher { get; }
        protected IUpdateCurrentLanguage UpdateCurrentLanguage { get; }

        public LocalizedNonContentControllerBase(ILanguageSegmentMatcher languageSegmentMatcher, IUpdateCurrentLanguage updateCurrentLanguage)
        {
            LanguageSegmentMatcher = languageSegmentMatcher;
            UpdateCurrentLanguage = updateCurrentLanguage;
        }

        protected void SetLanguageFromUrl(string url)
        {
            var segments = url.Trim('/').Split('/');
            string language = null;

            if (segments.Any())
            {
                var languageSegment = segments.First();
                if (languageSegment.ToLowerInvariant() == "dk") languageSegment = "da";
                LanguageSegmentMatcher.TryGetLanguageId(languageSegment, out language);
            }

            UpdateCurrentLanguage.UpdateLanguage(language);

            var resolvedLanguage = ContentLanguage.PreferredCulture.Name;
            Request.RequestContext.SetLanguage(resolvedLanguage);
        }
    }
}