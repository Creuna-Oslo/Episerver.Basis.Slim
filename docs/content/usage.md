## Creating solution from the template

Download the [latest release](http://github.com/Arthyon/Episerver.Basis.Slim/releases) and unzip the files in the directory you want to create the template.
Run build.cmd and after the script downloads the necessary dependencies you will be prompted for the solution name.

**Note**: Do not name part of your solution **\$solutionname\$** as it will mess with the namespace substitution regex.

The script will create the solution and clean up after itself.

After setting up the solution, a couple of steps are needed to set up Episerver correctly.

### Setting up the environment

- Create a database and add the connection string to ConnectionStrings.Debug.config.
- Get a license and put it in root of the web project (or override the licenseFilePath in EpiserverFramework.Debug.config)
- Create a site in IIS
- Navigate to said site and login to /Episerver/CMS/Admin as you co-user.
- Config -> Language settings: Add your languages and move the main language to the top (typically norwegian)
- Create a frontpage below Root in Edit Mode
- Manage Sites -> Add Site: Add new site with your host and choose newly created startpage as root

### Learn More
To learn more about what is included, take a look at the feature list in the menu or get an overview of the structure [here](structure.html).

### How it works

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
