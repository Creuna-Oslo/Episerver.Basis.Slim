# Enhancing the Template

The template can be opened as a regular solution using the solution file **src/Template/Creuna.Basis.Revisited.sln**
Add a ConnectionStrings.Debug.config to the Creuna.Basis.Revisited.Web/Configurations-folder and build.

You should then be ready to set up Episerver.

## What to enhance

The Basis template should only contain the bare necessities for a new episerver project. If your proposed enhancement is something only *some* projects will be using, consider contributing to [Creuna.Cookbook](https://creuna.visualstudio.com/Creuna.Cookbook) instead.

Other things to contribute beside new features are better ways to solve existing features, e.g. if Episerver releases a new and better way to do something, or a new feature in Episerver makes a nuget package obsolete.

## Code style guidelines

* Keep the code readable and terse. The user should be able to understand what the code does reasonably fast and without going through a lot of abstraction layers.
* Keep to the existing conventions so the project has a consistent style, e.g. put initializations in App_Start/[YourNewFeature]Initializer.cs


## Documentation
Remember to [document the new features](documentation.md). Without documenting the feature it is much harder to use and understand for the user, and the risk of reimplementing existing functionality increases.

Pull requests without proper documentation **will not** be accepted into master.

## Testing the new template

Usually, testing the scaffolding should not be necessary when only modifying the template, but could be useful to verify all namespaces are correctly replaced or any other edge cases you may encounter.

To generate a new build app with your template, run build.cmd without parameters from the root of the repository.
The template will be packaged, bundled with the scaffolding tool and put in the build-folder.

From there, run scaffolding.exe as normal and verify the output solution.

**NOTE:** All files not in source control will be removed when packaging the template (all files removed by *git clean -fxd ./src*), so remember to keep a copy of your Debug.config-files outside of the repository.