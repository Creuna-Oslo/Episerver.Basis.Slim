using Creuna.Basis.Revisited.Web.Models.Blocks;
using Creuna.Basis.Revisited.Web.Models.Pages.Base;

namespace Creuna.Basis.Revisited.Web.Models.ViewModels.Layout
{
    public class SeoSettingsViewModel
    {
        public SeoSettingsBlock SeoSettings { get; }
        public ContentPageBase CurrentPage { get; }

        public SeoSettingsViewModel(SeoSettingsBlock seoSettings, ContentPageBase currentPage)
        {
            SeoSettings = seoSettings;
            CurrentPage = currentPage;
        }
    }
}