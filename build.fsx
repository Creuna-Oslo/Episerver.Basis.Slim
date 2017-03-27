// --------------------------------------------------------------------------------------
// FAKE build script 
// --------------------------------------------------------------------------------------
// MY BUILD
#r @"packages/build/FAKE/tools/FakeLib.dll"
open Fake 
open Fake.Git
open Fake.AssemblyInfoFile
open Fake.ReleaseNotesHelper
open Fake.ConfigurationHelper
open System
open System.IO
open System.Text.RegularExpressions

// The name of the project 
// (used by attributes in AssemblyInfo, name of a NuGet package and directory in 'src')
let templateDir = "src/Template"
let buildScripts = "src/BuildScripts"
let defaultRootNamespace = "Creuna.Basis.Revisited"
let tokenNamespace = "$solutionname$" // This is also added as default root namespace in buildScripts/build.fsx

// Git configuration (used for publishing documentation in gh-pages branch)
// The profile where the project is posted 
let gitOwner = "Arthyon"
let gitHome = "https://github.com/" + gitOwner

// The name of the project on GitHub
let gitName = "Episerver.Basis.Slim"
let cloneUrl = "https://github.com/Arthyon/Episerver.Basis.Slim.git"

// Read additional information from the release notes document
Environment.CurrentDirectory <- __SOURCE_DIRECTORY__
let release = parseReleaseNotes (IO.File.ReadAllLines "RELEASE_NOTES.md")

let buildVersion = sprintf "%s-a%s" release.NugetVersion (DateTime.UtcNow.ToString "yyMMddHHmm")

let buildDir = "build/"
let tempDir = "temp/"

// --------------------------------------------------------------------------------------
// Clean build results

Target "Clean" (fun _ ->
    CleanDirs [ buildDir; tempDir ]
)

Target "CleanDocs" (fun _ ->
    CleanDirs ["docs/output"]
)

Target "CleanSources" (fun _ ->
    Shell.Exec("git", "clean -fxd", "src") |> ignore
)

// --------------------------------------------------------------------------------------
// Copy Templates and build scripts

Target "PackageTemplate" (fun _ ->
    ZipHelper.Zip (__SOURCE_DIRECTORY__ + "/" + tempDir) (buildDir + "Template.zip") (!! (tempDir + "/**"))
)

Target "CopyBuildScripts" (fun _ ->
    CopyDir buildDir buildScripts (fun _ -> true)
    
)
// ---------------------------
// Preprocessing of names

Target "CopyTemplateToTemporaryDirectory" (fun _ ->
     (CopyRecursive templateDir tempDir ) true |> ignore
)

type ReplacementFile = { OldName : string ; NewName : string ; isDirectory : bool }

let DetermineNewFileName file =
    let pattern = sprintf ".*%s.*" defaultRootNamespace
    let matches = Regex.Match(file, pattern)
    match matches.Success with
    | true -> let newName = Regex.Replace (file, defaultRootNamespace, tokenNamespace)
              let f = { OldName = file ; NewName = newName ; isDirectory = (isDirectory file )  }
              Some f
    | false -> None
    

Target "RenameDirectories" (fun _ ->
    let files = DirectoryInfo(tempDir).GetDirectories "*"

    let newFiles = files
                    |> Seq.map (fun dir -> dir.FullName)
                    |> Seq.choose DetermineNewFileName

    for file in newFiles do
        if  file.isDirectory then
            Rename file.NewName file.OldName 
)

Target "RenameFiles" (fun _ ->
    let files = !!(tempDir + "/**/*")

    let newFiles = files
                    |> Seq.choose DetermineNewFileName

    for file in newFiles do
        if not <| file.isDirectory then
            Rename file.NewName file.OldName
)


Target "ReplaceRootNamespace" (fun _ ->
    let files = !! (tempDir + "/**/*")
    RegexReplaceInFilesWithEncoding defaultRootNamespace tokenNamespace Text.Encoding.UTF8 files
)


// --------------------------------------------------------------------------------------
// Generate the documentation

let GenerateDocumentation release =
    let args = if release then ["--define:RELEASE"] else []
    executeFSIWithArgs "docs/tools" "generate.fsx" args [] |> ignore


Target "GenerateDocs" (fun _ ->
    GenerateDocumentation false
)

Target "GenerateDocsRelease" (fun _ ->
    GenerateDocumentation true    
) 

// --------------------------------------------------------------------------------------
// Release Scripts

Target "ReleaseDocs" (fun _ ->
    let tempDocsDir = "temp/gh-pages"
    CleanDir tempDocsDir
    Repository.cloneSingleBranch "" cloneUrl "gh-pages" tempDocsDir

    fullclean tempDocsDir
    CopyRecursive "docs/output" tempDocsDir true |> tracefn "%A"
    StageAll tempDocsDir
    Git.Commit.Commit tempDocsDir (sprintf "[skip ci] Update generated documentation for version %s" release.NugetVersion)
    Branches.push tempDocsDir
)

Target "KeepRunning" (fun _ ->
    use watcher = !! "docs/content/**/*.*" |> WatchChanges (fun changes ->
        GenerateDocumentation false
    )

    traceImportant "Waiting for help edits. Press any key to stop."

    System.Console.ReadKey() |> ignore

    watcher.Dispose()
)

// --------------------------------------------------------------------------------------
// Run main targets by default. Invoke 'build <Target>' to override

Target "Release" DoNothing
Target "Default" DoNothing
Target "WriteDocumentation" DoNothing

"Clean"
  ==> "CleanSources"
  ==> "CopyTemplateToTemporaryDirectory"
  ==> "RenameDirectories"
  ==> "RenameFiles"
  ==> "ReplaceRootNamespace"
  ==> "CopyBuildScripts"
  ==> "PackageTemplate"
  ==> "Default"


"CleanDocs"
  ==> "GenerateDocs" 
  ==> "KeepRunning"
  ==> "WriteDocumentation"

"CleanDocs"
 ==> "GenerateDocsRelease" 
 ==> "ReleaseDocs"
 ==> "Release"

RunTargetOrDefault "Default"
