using Creuna.Basis.Revisited.Web.Business;
using Creuna.Basis.Revisited.Web.Models.Blocks;
using EPiServer.Core;
using System.ComponentModel.DataAnnotations;

namespace Creuna.Basis.Revisited.Web.Models.Pages.Base
{
    public abstract class ContentPageBase : PageData
    {
        [Display(Name = "SEO Settings", Order = 10, GroupName = ApplicationConstants.TabNames.Seo)]
        public virtual SeoSettingsBlock SeoSettings { get; set; }
    }
}