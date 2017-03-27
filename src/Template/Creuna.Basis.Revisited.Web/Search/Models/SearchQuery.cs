namespace Creuna.Basis.Revisited.Web.Search.Models
{
    public class SearchQuery 
    {
        public string SearchTerm { get; }
        public int PageSize { get; }
        public int Page { get; }
        public string Language { get; }

        public SearchQuery(string term, int pageNumber, int pageSize, string language)
        {
            SearchTerm = term;
            Page = pageNumber;
            PageSize = pageSize;
            Language = language;
        }

    }
}