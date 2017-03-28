using System.Linq;
using System.Web.Mvc;
using EPiServer.Globalization;
using EPiServer.Web.Routing;
using EPiServer.Web.Routing.Segments;
using Creuna.Basis.Revisited.Web.Controllers.Base;

namespace Creuna.Basis.Revisited.Web.Controllers
{
    public class ErrorPagesController : LocalizedNonContentControllerBase
    {
        public ErrorPagesController(ILanguageSegmentMatcher languageSegmentMatcher, IUpdateCurrentLanguage updateCurrentLanguage)
            : base(languageSegmentMatcher, updateCurrentLanguage)
        {
        }

        public ActionResult NotFound()
        {
            HttpContext.Response.StatusCode = 404;
            SetLanguageFromUrl(Request.RawUrl);

            return View();
        }
    }
}