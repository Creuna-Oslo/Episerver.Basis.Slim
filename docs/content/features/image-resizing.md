## Image Resizing

The template has added the [RestImageResize-package](https://www.nuget.org/packages/RestImageResize.EPiServer/1.1.1/) that can resize requested images on the fly.

### Usage

The image resizer intercepts requests to images and transforms if one of these parameters are set:

- width
- height
- transform

Example of a request using image resizer:

```html
<img src="~/Content/Images/bigcat.JPG?width=1200&height=260&transform=fill" />
```

If a new size is requested, the transformed image is placed in [appDataPath]/transformedImages/ by default. This can be changed in EpiserverFramework.config by adjusting the virtualPath of the ImagesTransformVPP virtual path provider.

Helper methods to create a correct url from views are also included. These can be found in [Your solution name].Web.Business.Extensions.CmsExtensions (included by default in all views) and includes extension methods for UrlHelper.

Usage:

```csharp
@* From url string *@
@Url.Image(Model.ImagePath, width: 1200, height: 260)

@* From ImageData objectO *@
@Url.Image(Model.Image, width: 1200, height: 260)
```



See extended documentation here: [https://github.com/Romanets/RestImageResize](https://github.com/Romanets/RestImageResize).

### Relevant Web.config entries
The resizer uses OpenWaves.ImageTransformations under the hood, which adds a couple of lines to the web.config to work.

Under system.webServer, a new module called ImageTransformationsModule has been added:

```xml
<add name="ImageTransformationsModule" 
    type="OpenWaves.ImageTransformations.Web.WebImageTransformationModule, OpenWaves.ImageTransformations.Web" 
    preCondition="managedHandler" />
```

Under system.web/pages/controls a new assembly reference has been added

```xml
<add assembly="OpenWaves.ImageTransformations.Web" 
    namespace="OpenWaves.ImageTransformations.Web.Controls" 
    tagPrefix="ow" />
```


