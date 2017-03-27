using Creuna.Basis.Revisited.Web.Search.Models;
using EPiServer.Core;

namespace Creuna.Basis.Revisited.Web.Search
{
    public interface ISearchService
    {
        SearchResult<IContent> Search(SearchQuery query);
    }
}
