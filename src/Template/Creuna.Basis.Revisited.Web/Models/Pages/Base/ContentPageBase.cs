using Creuna.Basis.Revisited.Web.Business;
using Creuna.Basis.Revisited.Web.Models.Blocks;
using EPi.Libraries.BlockSearch.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace Creuna.Basis.Revisited.Web.Models.Pages.Base
{
    public abstract class ContentPageBase : PageData
    {
        [Display(Name = "SEO Settings", Order = 10, GroupName = TabNames.Seo)]
        public virtual SeoSettingsBlock SeoSettings { get; set; }

        [CultureSpecific]
        [ScaffoldColumn(false)]
        [Searchable]
        [AdditionalSearchContent]
        public virtual string SearchText { get; set; }
    }
}