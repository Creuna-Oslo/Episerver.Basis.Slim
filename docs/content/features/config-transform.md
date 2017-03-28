## Configuration Transformations

All base configuration files are in the folder **Configurations**.

When building the solution a configuration file matching the current configuration (if it exists) is applied as [Xdt-transforms](https://msdn.microsoft.com/en-us/library/dd465326(v=vs.110).aspx) to the base configuration file.

By default .Debug-files are ignored to each developer can have their own configuration (e.g. database connection strings or Find-index).

The .Release-files are prefilled with Octopus Deploy variables used for substitution with Octopus.
See the section about [Octopus Deploy](octopus.html) for more information.

### Technical details and adding a new config-file
The configuration transform is applied because of the [Your solution name].Web.wpp.targets-file. This is automatically run when in a project of type Web Project, and tells MSBuild to run the TransformXml-task from the Microsoft.Web.Publishing dll.

The ItemGroup-element determines which configurations are eligible for transform. If you need additional configuration files transformed add this to the ItemGroup-element:

```xml
<ConfigName Include="NewConfig">
    <Ext>config</Ext>
</ConfigName>
```

Optionally, you can also modify the csproj-file to nest the .Release and .Debug-files under the base.
