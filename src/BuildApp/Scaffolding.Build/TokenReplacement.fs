module TokenReplacement

open Fake 
open System
open System.Text.RegularExpressions
open System.IO


let ReplaceInFiles workingDir token replacement  =
    let files = !! (workingDir + "/**/*")
                |> Seq.where (fun f -> f.EndsWith(".zip") |> not)
   
    RegexReplaceInFilesWithEncoding token replacement Text.Encoding.UTF8 files


type ReplacementFile = { OldName : string ; NewName : string ; isDirectory : bool }

let DetermineNewFileName token (replacement : string ) (file : string) =
    let pattern = sprintf ".*%s.*" token
    let matches = Regex.Match(file, pattern)
    match matches.Success with
    | true -> let newName = Regex.Replace (file, token, replacement)
              let f = { OldName = file ; NewName = newName ; isDirectory = (isDirectory file )  }
              Some f
    | false -> None
    

let RenameDirectories workingDir collectFiles =
    let files = DirectoryInfo(workingDir).GetDirectories "*"

    let newFiles = files
                    |> Seq.map (fun dir -> dir.FullName)
                    |> Seq.choose collectFiles

    for file in newFiles do
        if  file.isDirectory then
            Rename file.NewName file.OldName 


let RenameFiles workingDir collectFiles =
    let files = !!(workingDir + "/**/*")
    let newFiles = files
                    |> Seq.choose collectFiles

    for file in newFiles do
        if not <| file.isDirectory then
            Rename file.NewName file.OldName


let ReplaceToken solutionDirectory tokenToReplace replacement =
    let collectFilesFunction = DetermineNewFileName tokenToReplace replacement

    RenameDirectories solutionDirectory collectFilesFunction
    RenameFiles solutionDirectory collectFilesFunction
    ReplaceInFiles solutionDirectory tokenToReplace replacement
    ()