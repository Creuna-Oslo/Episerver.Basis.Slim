## Razor

A new path is added to the view engine so it is possible to place pages in **~/Views/Pages/{controllername}.cshtml**, and blocks in **~/Views/Blocks/{controllername}.cshtml**.

e.g. **~/Views/Pages/FrontPage.cshtml**

## Razor Helpers
Some Razor helpers are included in the template. They are located ~/Business/Views.
That namespace is included by default in all views, so no need to add @using-statements.

### Razor Section with default content

In Razor, a section cannot have default content if not defined, unlike placeholders in Web Forms.

A method is included to make this possible. 
The approach is borrowed from [Phil Haack](http://haacked.com/archive/2011/03/05/defining-default-content-for-a-razor-layout-section.aspx/).

To create a new optional section with default content this snippet can be used in your views:

```csharp
@RenderSection("SomeSection", () => Html.Action("DefaultContent", "Layout"))
```

The return value of the anonymous method can be any MvcHtmlString.

### Strongly types Content Area Options

Episerver has a couple of magic parameters to pass to a content area to determine the rendering. As they are passed as an anonymous object, intellisense cannot help. This extension provides a class to provide strongly typed access to these properties.


```csharp
@Html.ContentArea(i => i.MyContentArea, new ContentAreaOptions
{
    ChildrenCssClass = "some-class",
    ChildrenCustomTag = "some-html-tag",
    CssClass = "some-class",
    CustomTag = "some-html-tag",
    RenderingTag = "some-rendering-tag"
})
```

### Url Helpers

A couple of helpers to manage urls are included.

- Converting a ContentReference, relative url or EPiServer.Url to absolute url
- Helper to create link to a [resized image](image-resizing.html) from url or ImageData
- Method to replace single query parameter in url
- Get language segment from current url