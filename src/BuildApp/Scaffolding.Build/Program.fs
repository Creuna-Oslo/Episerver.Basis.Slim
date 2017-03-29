open System.IO
open System
open System.Security.Principal
open Build



let IsValidDirectory directoryPath =
    Directory.Exists directoryPath

let DetermineBaseDirectory argv =
    match argv with
    | head :: tail when IsValidDirectory head -> head
    | _ -> Environment.CurrentDirectory



[<EntryPoint>]
let main argv = 
    let scaffoldingDirectory = argv |> Array.toList |> DetermineBaseDirectory
    printfn "Scaffolding to %s" scaffoldingDirectory
    StartBuild scaffoldingDirectory
    0
