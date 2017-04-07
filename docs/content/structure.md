## Understanding the structure

The solution structure should be familiar to most.

- The Api-folder is meant to contain [WebApi-controllers](features/web-api.html).
- App_Data is the default folder for logs, [search indices](features/search.html) and [resized images](features/image-resizing.html)
- App_Start contains all initialization modules
- Business is used for various factories, attributes etc. to extend the episerver functionality
- Configurations contains all base and transform-files for [configuration transforms](features/config-transform.html)
- Controllers contains all page and block controllers
- IndexingService is used in development when using [Episerver Search](features/search.html)
- lang contains all translation files
- Models contains all pages, blocks and view models
- modules is an Episerver folder
- Search contains the ISearchService-interface and a Episerver Search-implementation
- Static is meant to contain all frontend files and will be included by default by [Octopack](features/octopus.html)
- Views contains all template files. Page templates can be created as **Pages/{controllername}.cshtml** or **{controllername}/Index.cshtml**.


The solution also contains a nuget.config-file that references the normal Nuget-feed, Episerver's feed and the Creuna myget-feed by default.