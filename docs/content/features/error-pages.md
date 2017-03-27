## Error Pages

The template contains controllers for 404 and 500-pages.
These can be used to show custom ontent to the user when encountering a missing page or a server error.

MvcRoutesInitializer.cs initializes two routes that is routing to the ErrorPagesController.cs.

They are setting the language for the request so translations will work.

They are enabled in web.config under system.webServer:

    [lang=xml]
     <httpErrors errorMode="Custom" existingResponse="Auto">
      <remove statusCode="404" />
      <remove statusCode="500" />
      <error statusCode="404" path="/error/404" responseMode="ExecuteURL" />
      <error statusCode="500" path="/error/500" responseMode="ExecuteURL" />
    </httpErrors>


