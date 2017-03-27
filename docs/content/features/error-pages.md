## Error Pages

The template contains handling of 404 and 500 errors.

### 404
The 404 page is routed to a controller used to show custom content. 

MvcRoutesInitializer.cs initializes a route that is routing to the ErrorPagesController.cs.

This controller is setting the language for the request so translations will work. Normal best practices should apply, so limit the external references from the 404-page.
In case one of the assets are returning 404 we won't end up in a loop as the 404 page is requesting a non-existing resource.

### 500

The 500 error is not routed through a controller. A 500 error can occur in multiple ways, and if the error occurs before the pipeline is initialized and we cannot initialize routing correctly (e.g. if there's a syntax error in web.config) we're in limbo.

The easiest way is therefore to return a static file for 500-errors. It is possible to add the 500 error to the ErrorPages controller if needed, but a reasonable fallback strategy should be developed.
(If you do develop this, please contribute it back to this template!)


### Adding additional handlers

Add a new route to an action on the ErrorPages controller and a new view under /Views/ErrorPages.

The redirect is enabled in web.config under system.webServer:

    [lang=xml]
     <httpErrors errorMode="Custom" existingResponse="Auto">
      <remove statusCode="404" />
      <remove statusCode="500" />
      <error statusCode="404" path="/error/404" responseMode="ExecuteURL" />
      <error statusCode="500" path="500.html" responseMode="File" />
    </httpErrors>


