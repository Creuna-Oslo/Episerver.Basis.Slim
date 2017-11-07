## Search

Episerver Search is bundled as a default search solution. Many projects are using Find, so how to remove Episerver Search is also documented here.

A [nuget package](https://github.com/jstemerdink/EPi.Libraries.BlockSearch) is included to make blocks searchable as well. If you want to exclude a property on a block from being indexed, add the attribute [Searchable(false)] to it.

### Enabling search
Episerver Search is disabled by default, as a manual configuration change is needed without crashing the initialization.

In EpiserverSearch.config change the active-attribute to true and change the baseUri to point to your local address (projectname.local, localhost:12345 or whatever you use)

e.g. **http://localhost:8888/IndexingService/IndexingService.svc**

To rebuild the index after setting up, use this hidden tool:
http://sitehost/EPiServer/CMS/Admin/IndexContent.aspx


### Rendering search results
A search page using Episerver Search is included and can be created in the hierarchy to use the search.

Each page can determine how its search result should be rendered by implementing a partial controller for the rendering tag Search. 

```csharp
[TemplateDescriptor(TemplateTypeCategory = TemplateTypeCategories.MvcPartialController, 
    Inherited = true, 
    AvailableWithoutTag = false, 
    Tags = new[] { ApplicationConstants.RenderingTags.Search })]
public class MyNewPageSearchResultController : PartialContentController<MyNewPage>
{
    public override ActionResult Index(MyNewPage currentPage) 
        => PartialView("~/Views/MyNewPage/SearchResult.cshtml", currentPage);
}
```

If no template is specified it will fall back to a generic search template found at ~/Views/SearchPage/GenericSearchResult.cshtml.


### Using search in production
You should not be using the local indexing service when hosting in production, especially not in Azure or behind a load balancer. In this situation, all sites should share the same index to avoid drift.

A separate site with only the indexing service should be set up in this situation.

The configuration transformation is set up to remove the local server when building in Release and inject the path to a remote search server using Octopus substitution syntax. See the [Octopus-section](octopus.html) for more information.

### Removing Episerver Search

If using other search solutions, Episerver search can safely be removed. 
In the folder Configurations, remove EpiserverSearch.config, EpiserverSearchIndexingService.config and the equivalent environment transformation files (.Release etc).

In Web.config, remove the two sections: episerver.search and episerver.search.indexingservice, as well as the IndexingServiceCustomBinding and and the Location element regarding the path IndexingService/IndexingService.svc.
In Web.Release.config remove the location-transform element.

Remove the nuget package EPi.Libraries.BlockSearch and the SearchText-property in ContentPageBase.

Remove the nuget package EPiServer.Search and remove the IndexingService-folder if the uninstallation process did not remove it.

In the .targets-file, remove the two ConfigName-elements for EPiServerSearch and EPiServerSearchIndexingService.

