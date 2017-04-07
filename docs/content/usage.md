## Creating a solution from the template

There are three possible ways to scaffold the template.

**Note**: Do not name part of your solution **\$solutionname\$** as it will mess with the namespace substitution regex.

### 1. The easy way
Download the [latest scaffolding.exe](http://github.com/Creuna-Oslo/Episerver.Basis.Slim/releases).
Run it from the directory you want to scaffold into, or pass the path to a directory as the first argument (It defaults to current working directory if first argument is missing or not a valid directory).

You will be prompted for the solution name.

### 2. If you want to inspect sources
**NOTE**: This will be deprecated in the next release. The scaffolding tool will be the main way to distribute the template, mostly to avoid maintaining two code bases. The project is open source anyway, so the paranoid people can compile their own tool.


Download the [latest Scripts.zip](http://github.com/Creuna-Oslo/Episerver.Basis.Slim/releases).
Unzip the files in the directory you want to create the template.
build.fsx describes the steps the script will execute, so you can be check it out yourself.
Run build.cmd and after the script downloads the necessary dependencies you will be prompted for the solution name.

The script will create the solution and clean up after itself, deleting all files, including build.cmd.
If you want to know how it works, [this page](how-it-works.html) is for you.

### 3. DIY
Clone the repository and build the scripts and exe yourself :)

Please see the [Contribute](contribute.html)-page for details about the build process and moving parts.

## Setting up the environment

After setting up the solution, a couple of steps are needed to set up Episerver correctly.

- Create a database and add the connection string to ConnectionStrings.config, or create a ConnectionString.Debug.config and add it as a transformation.
- Get a license and put it in root of the web project (or override the licenseFilePath in EpiserverFramework.Debug.config)
- Create a site in IIS and make the application pool user db_owner of the database created earlier
- Navigate to the site and log in to /Episerver/CMS/Admin as your co-user.
- Config -> Language settings: Add your languages and move the main language to the top (typically norwegian)
- Create a frontpage below Root in Edit Mode
- Manage Sites -> Add Site: Add new site with your host and choose newly created startpage as root

**NOTE**: If you are using Episerver Find, follow the instructions for how to remove Episerver Search [here](features/search.html)

## Learn More
To learn more about what is included, take a look at the feature list in the menu or get an overview of the structure [here](structure.html).
