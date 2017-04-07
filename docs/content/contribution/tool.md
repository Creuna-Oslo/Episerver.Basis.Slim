# Improving the scaffolding tool

The scaffolding tool is written using FAKE in an F# console application.

Create a template.zip by running build.cmd in the root of the repository like this:

```
build.cmd PackageTemplate
```

and copy the template.zip in the root of src/BuildApp/Scaffolding.Build.

Now it is possible to run the project directly from VS and debug the process.

FAKE has a number of helpers to complete almost any task, and you can of course use all possible functionality in F# as well. 

References:

* [A list of the helpers in FAKE](http://fsharp.github.io/FAKE/apidocs/index.html)
* [Getting started with FAKE](http://fsharp.github.io/FAKE/gettingstarted.html)

### A quirk to be aware of
We have an indirect dependency between the build app and the script that builds it.

The same way to replace the tokens are used both when packaging the template (moving from Creuna.Basis.Revisited to \$solutionname\$) and when extracting the template (moving from \$solutionname\$ to the user's choice of name).

If your improvement touches upon that part of the tool, make sure the change is also reflected in the root build.fsx

## Testing the tool
Run build.cmd without parameters to create a new scaffolding.exe in the build-folder.

We are using IL Repack to merge all dependent dlls into the exe-file. If your enhancement uses an additional dll, make sure it is placed in the bin-folder of the app after build.
