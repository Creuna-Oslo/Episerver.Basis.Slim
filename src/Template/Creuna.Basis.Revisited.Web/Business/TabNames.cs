using EPiServer.DataAnnotations;
using EPiServer.Security;
using System.ComponentModel.DataAnnotations;

namespace Creuna.Basis.Revisited.Web.Business
{
    [GroupDefinitions]
    public class TabNames
    {
        [Display(Name = "SEO", Order = 1000)]
        [RequiredAccess(AccessLevel.Edit)]
        public const string Seo = "SEO";
    }
}