using Creuna.Basis.Revisited.Web.Models.Pages.Base;
using Creuna.Basis.Revisited.Web.Models.ViewModels.Layout;
using EPiServer.Core;
using System.Web.Mvc;

namespace Creuna.Basis.Revisited.Web.Controllers
{
    public class LayoutController : Controller
    {
        public ActionResult Metadata(PageData currentPage)
        {
            var contentPage = currentPage as ContentPageBase;
            if (contentPage == null)
                return new EmptyResult();

            return PartialView(new SeoSettingsViewModel(contentPage.SeoSettings, contentPage));
        }

        public ActionResult Header(PageData currentPage)
            => PartialView(new HeaderViewModel(currentPage));

        public ActionResult Footer(PageData currentPage)
            => PartialView(new FooterViewModel(currentPage));
    }
}