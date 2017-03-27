#r @"packages/build/FAKE/tools/FakeLib.dll"
open Fake 
open Fake.Git
open Fake.AssemblyInfoFile
open Fake.ReleaseNotesHelper
open Fake.ConfigurationHelper
open System
open System.IO
open System.Text.RegularExpressions

let defaultRootNameSpace = "\$solutionname\$" //Escaped for regex

let mutable solution = "MyProject"

Environment.CurrentDirectory <- __SOURCE_DIRECTORY__

let GetSolutionName () =
   let namePrompt = sprintf "Solution name (default: %s, so you should specify a name): " solution
   let chosenName = UserInputHelper.getUserInput namePrompt 
   match String.IsNullOrEmpty chosenName with
   | true -> solution
   | false -> chosenName   
                

Target "CollectInfo" (fun _ ->
    solution <- GetSolutionName ()
)

Target "UnzipFiles" (fun _ ->
    ZipHelper.Unzip solution "Template.zip"
)

Target "ReplaceRootNameSpace" (fun _ ->
    let files = !! (solution + "/**/*")
   
    RegexReplaceInFilesWithEncoding defaultRootNameSpace solution Text.Encoding.UTF8 files
)

type ReplacementFile = { OldName : string ; NewName : string ; isDirectory : bool }

let DetermineNewFileName file =
    let pattern = sprintf ".*%s.*" defaultRootNameSpace
    let matches = Regex.Match(file, pattern)
    match matches.Success with
    | true -> let newName = Regex.Replace (file, defaultRootNameSpace, solution)
              let f = { OldName = file ; NewName = newName ; isDirectory = (isDirectory file )  }
              Some f
    | false -> None
    

Target "RenameDirectories" (fun _ ->
    let files = DirectoryInfo(solution).GetDirectories "*"

    let newFiles = files
                    |> Seq.map (fun dir -> dir.FullName)
                    |> Seq.choose DetermineNewFileName

    for file in newFiles do
        if  file.isDirectory then
            Rename file.NewName file.OldName 
)

Target "RenameFiles" (fun _ ->
    let files = !!(solution + "/**/*")
    let newFiles = files
                    |> Seq.choose DetermineNewFileName

    for file in newFiles do
        if not <| file.isDirectory then
            Rename file.NewName file.OldName
)

Target "CleanUp" (fun _ ->
    DeleteDirs [ ".paket" ]
    DeleteFiles [ "paket.dependencies"; "paket.lock"; "Template.zip"; "build.fsx"]

)


// --------------------------------------------------------------------------------------
// Run main targets by default. Invoke 'build <Target>' to override

Target "Start" DoNothing
Target "Default" DoNothing

"Start"
  ==> "CollectInfo"
  ==> "UnzipFiles"
  ==> "RenameDirectories"
  ==> "RenameFiles"
  ==> "ReplaceRootNameSpace"
  ==> "CleanUp"
  ==> "Default"

Run "Default"
