## Image Resizing

The template has added the [Image Resizer-package](https://imageresizing.net) that can resize requested images on the fly. An [additional plugin](https://github.com/valdisiljuconoks/ImageResizer.Plugins.EPiServerBlobReader) to handle episerver-files has also been added.

### Usage

The image resizer intercepts requests to images and transforms if one of these parameters are set:

- width
- height
- mode

Example of a request using image resizer:

```html
<img src="/globalassets/logo.png?width=1200&height=260&mode=crop" />
```

Helper methods to create a correct url from views are also included in the episerver plugin. The namespace for these is automatically used for all views.

Usage:

```csharp
@* From url string *@
<img src="@Html.ResizeImage(Model.ImagePath, width: 1200, height: 260)" />

@* From ImageData object *@
<img src="@Html.ResizeImage(Model.Image, width: 1200, height: 260)" />

@* Using Fluent syntax *@
<img src="@Html.ResizeImage(Model.Image).Width(1200)" />
```

See extended documentation here: [https://github.com/valdisiljuconoks/ImageResizer.Plugins.EPiServerBlobReader](https://github.com/valdisiljuconoks/ImageResizer.Plugins.EPiServerBlobReader).

### Plugins
There are a lot of plugins available for the image resizer.
**Note:** Some require a license to use.

A list of all plugins can be found [here](https://imageresizing.net/plugins).

To install a new plugin, add its nuget-package and add a line for it in ImageResizerInitializer.cs

```csharp
new PluginName().Install(Config.Current);
```


### Relevant Web.config entries
The resizer uses OpenWaves.ImageTransformations under the hood, which adds a couple of lines to the web.config to work.

Under system.webServer, a new module called ImageResizingModule has been added:

```xml
<add name="ImageResizingModule" type="ImageResizer.InterceptModule" />
```

Under system.web/httpModules a new module has been registered

```xml
<add name="ImageResizingModule" type="ImageResizer.InterceptModule" />
```

