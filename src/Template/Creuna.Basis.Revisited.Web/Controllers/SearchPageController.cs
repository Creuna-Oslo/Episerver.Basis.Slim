using Creuna.Basis.Revisited.Web.Business;
using Creuna.Basis.Revisited.Web.Business.Views;
using Creuna.Basis.Revisited.Web.Models.Pages;
using Creuna.Basis.Revisited.Web.Models.ViewModels;
using Creuna.Basis.Revisited.Web.Search;
using Creuna.Basis.Revisited.Web.Search.Models;
using EPiServer.Core;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Framework.Web;
using EPiServer.Web.Mvc;
using System.Web.Mvc;

namespace Creuna.Basis.Revisited.Web.Controllers
{
    public class SearchPageController : PageController<SearchPage>
    {
        ISearchService SearchService { get; }

        public SearchPageController(ISearchService searchService)
        {
            SearchService = searchService;
        }

        public ActionResult Index(SearchPage currentPage, string q, int page = 1)
        {
            var query = new SearchQuery(q, page, currentPage.PageSize, UrlHelperExtensions.LanguageURLSegment());
            var result = SearchService.Search(query);

            return View(new SearchPageViewModel(q, query.Page, query.PageSize, currentPage, result));
        }
    }

    /// <summary>
    /// Fallback controller for search results without their own rendering
    /// </summary>
    [TemplateDescriptor(TemplateTypeCategory = TemplateTypeCategories.MvcPartialController, Inherited = true, AvailableWithoutTag = false, Tags = new[] { ApplicationConstants.RenderingTags.Search })]
    public class GenericSearchContentController : PartialContentController<IContent>
    {
        public override ActionResult Index(IContent currentPage) => PartialView("~/Views/Shared/SearchResults/GenericSearchResult.cshtml", currentPage);
    }
}