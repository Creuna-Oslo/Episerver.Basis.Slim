using EPiServer.Core;

namespace Creuna.Basis.Revisited.Web.Models.ViewModels.Layout
{
    public class FooterViewModel
    {
        public PageData CurrentPage { get; }

        public FooterViewModel(PageData currentPage)
        {
            CurrentPage = currentPage;
        }
    }
}