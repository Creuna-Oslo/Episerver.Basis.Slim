using Creuna.Basis.Revisited.Web.Models.Pages;
using Creuna.Basis.Revisited.Web.Search.Models;
using EPiServer.Core;
using System;
using System.Collections.Generic;

namespace Creuna.Basis.Revisited.Web.Models.ViewModels
{
    public class SearchPageViewModel
    {
        public string Query { get; }
        public int CurrentPageNumber { get; }
        public int TotalPages { get; }
        public IReadOnlyCollection<IContent> Items { get; }

        public SearchPage CurrentPage { get; }

        public SearchPageViewModel(string query, int currentPageNumber, int pageSize, SearchPage currentPage, SearchResult<IContent> results)
        {
            Query = query;
            CurrentPage = currentPage;
            CurrentPageNumber = currentPageNumber;
            TotalPages = (int)Math.Ceiling((double)results.TotalCount / pageSize);
            Items = results.Items;
        }
    }
}