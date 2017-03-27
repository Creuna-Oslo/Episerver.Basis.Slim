using EPiServer.Core;

namespace Creuna.Basis.Revisited.Web.Models.ViewModels.Layout
{
    public class HeaderViewModel
    {
        public PageData CurrentPage { get; }

        public HeaderViewModel(PageData currentPage)
        {
            CurrentPage = currentPage;
        }
    }
}