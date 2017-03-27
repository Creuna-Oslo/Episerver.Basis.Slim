using EPiServer.Core;
using EPiServer.DataAnnotations;
using EPiServer.Framework.DataAnnotations;

namespace Creuna.Basis.Revisited.Web.Models.Media
{
    [ContentType(DisplayName = "[Media] Image File", GUID = "ee574c36-9e32-4c0a-bc2a-20c2a5954863", Description = "")]
    [MediaDescriptor(ExtensionString = "jpg,jpeg,jpe,ico,gif,bmp,png,svg")]
    public class ImageFile : ImageData
    {

    }
}