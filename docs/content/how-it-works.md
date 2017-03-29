## How the build scripts work

These are the files used to create the solution:

- build.cmd
- build.fsx
- Template.zip
- .paket/paket.bootstrapper.exe
- paket.dependencies
- paket.lock

If you are familiar with the workflow when using FAKE and paket, this will be straightforward to understand.

1. build.cmd downloads executes paket.bootstrapper which checks for the latest version of paket.exe and downloads it.
2. Paket.exe inspects paket.lock for all dependencies, in this case only FAKE, and downloads them.
3. FAKE is executed using build.fsx.
4. Build.fsx unzips the template and substitutes all namespaces and filenames with the entered solution name.
5. build.cmd deletes all files and folders used during the build.

