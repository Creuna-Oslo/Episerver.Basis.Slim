using System.Linq;
using System.Web.Mvc;
using EPiServer.Globalization;
using EPiServer.Web.Routing;
using EPiServer.Web.Routing.Segments;

namespace Creuna.Basis.Revisited.Web.Controllers
{
    public class ErrorPagesController : Controller
    {
        ILanguageSegmentMatcher LanguageSegmentMatcher { get; }
        IUpdateCurrentLanguage UpdateCurrentLanguage { get; }

        public ErrorPagesController(ILanguageSegmentMatcher languageSegmentMatcher, IUpdateCurrentLanguage updateCurrentLanguage)
        {
            LanguageSegmentMatcher = languageSegmentMatcher;
            UpdateCurrentLanguage = updateCurrentLanguage;
        }

        public ActionResult NotFound()
        {
            HttpContext.Response.StatusCode = 404;
            SetLanguageFromurl();

            return View();
        }

        void SetLanguageFromurl()
        {
            var url = Request.RawUrl;
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