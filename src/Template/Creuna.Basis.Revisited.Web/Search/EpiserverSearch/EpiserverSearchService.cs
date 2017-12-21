using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Creuna.Basis.Revisited.Web.Search.Models;
using EPiServer.Core;
using EPiServer.Search;
using EPiServer.Security;
using EPiServer.Search.Queries.Lucene;
using EPiServer.Search.Queries;
using Creuna.Basis.Revisited.Web.Search.EpiserverSearch.Expressions;

namespace Creuna.Basis.Revisited.Web.Search.EpiserverSearch
{
    public class EpiserverSearchService : ISearchService
    {
        readonly ContentSearchHandler _contentSearchHandler;
        readonly SearchHandler _searchHandler;

        public EpiserverSearchService(SearchHandler searchHandler, ContentSearchHandler contentSearchHandler)
        {
            _contentSearchHandler = contentSearchHandler;
            _searchHandler = searchHandler;
        }

        public SearchResult<IContent> Search(SearchQuery query)
        {
            var ftsResults = FtsSearch(query);

            if (string.IsNullOrEmpty(query.SearchTerm))
                return SearchResult<IContent>.NoHits();

            var result = ftsResults.IndexResponseItems.Select(IndexItemToPageData).ToList();
            var totalCount = ftsResults.TotalHits;

            return new SearchResult<IContent>(result, totalCount);
        }

        SearchResults FtsSearch(SearchQuery query)
        {
            // prepare filters
            var filters = new List<Func<IQueryExpression>>
                {
                    GetSecurityFilter,
                    GetStatusFilter,
                    () => GetTermFilter(query.SearchTerm),
                    GetItemTypeFilter,
                    () => GetLanguageFilter(query.Language)
                };

            // build query by adding expressions
            var searchQuery = new GroupQuery(LuceneOperator.AND);
            filters.ForEach(x =>
            {
                var q = x();
                if (q != null)
                {
                    searchQuery.QueryExpressions.Add(q);
                }
            });

            // Get search results from indexing service via the FTS Client
            var result = _searchHandler.GetSearchResults(searchQuery, query.Page, query.PageSize);
            return result;
        }

        FieldQuery GetLanguageFilter(string language)
        {
            return new FieldQuery(language, Field.Culture);
        }

        AccessControlListQuery GetSecurityFilter()
        {
            var aclQuery = new AccessControlListQuery();
            aclQuery.AddAclForUser(PrincipalInfo.Current, HttpContext.Current);

            return aclQuery;
        }

        GroupQuery GetTermFilter(string term, Field field = Field.Default)
        {
            if (string.IsNullOrEmpty(term))
            {
                return null;
            }

            var result = new GroupQuery(LuceneOperator.OR);
            result.QueryExpressions.Add(new FieldQuery(term, field));
            return result;
        }

        IQueryExpression GetStatusFilter()
        {
            var statusFilterExpression = new GroupQuery(LuceneOperator.AND);

            var underTheTrashQuery = new VirtualPathQuery();
            underTheTrashQuery.AddContentNodes(ContentReference.WasteBasket);

            statusFilterExpression.QueryExpressions.Add(new NotQueryExpression(underTheTrashQuery));
            statusFilterExpression.QueryExpressions.Add(new ItemStatusQuery(ItemStatus.Approved));

            return statusFilterExpression;
        }

        IQueryExpression GetItemTypeFilter()
        {
            var groupExpression = new GroupQuery(LuceneOperator.OR);
            groupExpression.QueryExpressions.Add(new ContentQuery<PageData>());
            groupExpression.QueryExpressions.Add(new NotQueryExpression(new ContentQuery<IContent>()));
            return groupExpression;
        }

        IContent IndexItemToPageData(IndexResponseItem item) => _contentSearchHandler.GetContent<IContent>(item);
    }
}