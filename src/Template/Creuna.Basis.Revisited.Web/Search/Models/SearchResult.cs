using System.Collections.Generic;
using System.Linq;

namespace Creuna.Basis.Revisited.Web.Search.Models
{
    public class SearchResult<TItem>
    {
        public IReadOnlyCollection<TItem> Items { get; }
        public int TotalCount { get; }

        public SearchResult(IEnumerable<TItem> items, int totalCount)
        {
            Items = items.ToList();
            TotalCount = totalCount;
        }

        public static SearchResult<TItem> NoHits() => new SearchResult<TItem>(new TItem[0], 0);
    }
}