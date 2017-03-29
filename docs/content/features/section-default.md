## Razor Section with default content

In Razor, a section cannot have default content if not defined, unlike placeholders in Web Forms.

A method is included to make this possible. 
The approach is borrowed from [Phil Haack](http://haacked.com/archive/2011/03/05/defining-default-content-for-a-razor-layout-section.aspx/).

To create a new optional section with default content this snippet can be used in your views:

```csharp
@RenderSection("SomeSection", () => Html.Action("DefaultContent", "Layout"))
```

The return value of the anonymous method can be any MvcHtmlString.