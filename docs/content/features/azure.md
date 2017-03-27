## Azure

The nuget package for hosting in Azure is installed by default. For an in depth explanation of which Azure-services are needed, refer to [this guide](http://world.episerver.com/documentation/Items/Developers-Guide/Episerver-CMS/75/Deployment/Deployment-scenarios/Deploying-to-Azure-websites/).

It is set up so blob storage and event bus is only enabled when building in release, so each developer does not need to create their own Azure environment.

What you need to do in the solution is to add the connection strings to Service Bus and Blob storage to your Octopus-server. See [Octopus Package Generation](octopus.html) for more information.

**Note**: The default value for the blob container name is **mysitemedia** and the default name for the event bus is **MySiteEvents**. These values are not controlled by config settings, so this should be changed manually in EpiserverFramework.Release.config if needed.

### Removing Azure settings

To remove all traces of Azure-compatibility, perform these steps:

- Remove the EPiServer.Azure-package
- Remove the blob- and event-elements from EpiserverFramework.Release.config.
- Remove the EPiServerAzureBlobs and EPiServerAzureEvents entries from ConnectionStrings.Release.config 