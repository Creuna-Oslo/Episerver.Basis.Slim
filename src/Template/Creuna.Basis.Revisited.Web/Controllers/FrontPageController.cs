using Creuna.Basis.Revisited.Web.Models.Pages;
using EPiServer.Web.Mvc;
using System.Web.Mvc;

namespace Creuna.Basis.Revisited.Web.Controllers
{
    public class FrontPageController : PageController<FrontPage>
    {
        [ContentOutputCache]
        public ActionResult Index(FrontPage currentPage)
            => View(currentPage);
    }
}