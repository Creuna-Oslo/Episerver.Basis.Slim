module Build

open Fake 
open Fake.Git
open Fake.AssemblyInfoFile
open Fake.ReleaseNotesHelper
open Fake.ConfigurationHelper
open System
open System.IO
open System.Text.RegularExpressions
open System.Resources
let defaultRootNameSpace = "\$solutionname\$" //Escaped for regex

let DefaultSolutionName = "MyProject"

let GetSolutionName () =
   let namePrompt = sprintf "Solution name (default: %s, so you should specify a name): " DefaultSolutionName
   let chosenName = UserInputHelper.getUserInput namePrompt 
   match String.IsNullOrEmpty chosenName with
   | true -> DefaultSolutionName
   | false -> chosenName   
           

let StartBuild workingDir =
    let solutionName = GetSolutionName ()
    let targetDirectory = Path.Combine(workingDir, solutionName)

    TemplateExtraction.ExtractTemplate targetDirectory
    TokenReplacement.ReplaceToken targetDirectory defaultRootNameSpace solutionName
    ()