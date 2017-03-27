## Octopus Package Generation

Octopus compatibility is included in the template for creating nuget packages from a CI-system and substituting variables in configuration files.

### Generating Nuget-packages
The package [Octopack](https://octopus.com/docs/packaging-applications/nuget-packages/using-octopack) is added to the project. This adds a custom MSBuild target that packages your application.
The file [Your solution name].Web.nuspec is also added to also include the contents of the folder Static (where all frontend resources should be placed).

When building the solution, e.g. in TeamCity, the resulting nuget package will be in /obj/octopacked and to publish to Octopus' nuget feed, a build step of type Nuget Publish can be added.

The packages can then be located using this glob pattern: **\*\*\\obj\\octopacked\\\*.nupkg**

### Transforming configuration
The .Release-configurations include variables that Octopus will subsitute if the Package Step Substitute variables in files are active.

The variables shipped by default is:

- **Db**: Connection string to Episerver Database
- **AppDataPath**: Path to app data folder
- **LicenseFilePath**: Path to license file
- **LogDirectory**: Path to directory for log files
- **SearchServer**: Url to server where episerver search is hosted (include the IndexingService/IndexingService.svc-part)

- **BlobStorage**: When using Azure, connection string to blob storage
- **EventBus**: When using Azure, connection string to event bus
