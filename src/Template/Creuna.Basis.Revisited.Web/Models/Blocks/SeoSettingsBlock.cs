using Creuna.Basis.Revisited.Web.Business.SelectionFactories;
using Creuna.Basis.Revisited.Web.Models.Enums;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Web;
using System.ComponentModel.DataAnnotations;

namespace Creuna.Basis.Revisited.Web.Models.Blocks
{
    [ContentType(GUID = "{81F04FFD-8FA0-4339-9D0C-1B75D0981360}", AvailableInEditMode = false, Order = 10)]
    public class SeoSettingsBlock : BlockData
    {
        [Display(
            ShortName = "Title",
            Name = "Title",
            Description = "Page Name is used as a fallback",
            Order = 10,
            GroupName = SystemTabNames.Content)]
        [CultureSpecific]
        public virtual string Title { get; set; }

        [Display(
            ShortName = "Meta description",
            Name = "Meta description",
            Order = 20)]
        [UIHint(UIHint.LongString)]
        [CultureSpecific]
        public virtual string MetaDescription { get; set; }

        [Display(
            ShortName = "Open Graph: Title",
            Name = "Open Graph: Title</br>(Recommended max: 70 characters)",
            Description = "This is the title of the piece of content.",
            Order = 30,
            GroupName = SystemTabNames.Content)]
        [CultureSpecific]
        public virtual string OpenGraphTitle { get; set; }

        [Display(
            ShortName = "Open Graph: Description",
            Name = "Open Graph: Description<br/>(Recommended max: 155 characters)",
            Description = "A one to two sentence description of the page.",
            Order = 40,
            GroupName = SystemTabNames.Content)]
        [UIHint(UIHint.LongString)]
        [CultureSpecific]
        public virtual string OpenGraphDescription { get; set; }

        [Display(
             ShortName = "Open Graph: Type",
             Name = "Open Graph: Type",
             Description = "This is the type of object your piece of content is.",
             Order = 50,
            GroupName = SystemTabNames.Content)]
        [SelectOne(SelectionFactoryType = typeof(EnumSelectionFactory<OpenGraphType>))]
        public virtual OpenGraphType OpenGraphType { get; set; }

        [Display(
            ShortName = "Open Graph: Image",
            Name = "Open Graph: Image<br/>Main image (if any) will be used as a fallback. ",
            Description = " This is the image that social media will show in the screenshot of the content",
            Order = 60,
            GroupName = SystemTabNames.Content)]
        [UIHint(UIHint.Image)]
        public virtual Url OpenGraphImage { get; set; }
    }

}