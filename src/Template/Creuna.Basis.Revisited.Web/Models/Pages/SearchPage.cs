using Creuna.Basis.Revisited.Web.Business;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace Creuna.Basis.Revisited.Web.Models.Pages
{
    [ContentType(
        DisplayName = "Search", 
        Description = "Shows search results", 
        GUID = "{82BB63AF-4DA5-4C44-AF07-FA75BA35AE3A}", 
        GroupName = ApplicationConstants.PageGroupNames.System, 
        Order = 20)]
    public class SearchPage : PageData
    {
        [Display(Name = "Page Size", Order = 10, GroupName = SystemTabNames.Settings)]
        public virtual int PageSize { get; set; }

        public override void SetDefaultValues(ContentType contentType)
        {
            PageSize = 20;
            base.SetDefaultValues(contentType);
        }
    }
}