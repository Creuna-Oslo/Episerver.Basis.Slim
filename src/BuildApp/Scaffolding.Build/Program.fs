open System.IO
open System
open System.Security.Principal
open Build

let IsValidDirectory directoryPath =
    Directory.Exists directoryPath


let DetermineBaseDirectory argv =
    match argv with
    | head :: tail when (head = "--version") -> printf "Version: %s" AssemblyVersionInformation.AssemblyVersion
                                                None
    | head :: tail when IsValidDirectory head -> Some head
    | _ -> Some Environment.CurrentDirectory

let PrintVersionInformation () =
    printfn "%s" AssemblyVersionInformation.AssemblyTitle
    printfn "%s" AssemblyVersionInformation.AssemblyDescription
    printfn "Version: %s" AssemblyVersionInformation.AssemblyVersion


[<EntryPoint>]
let main argv = 
    
    let scaffoldingDirectory = argv |> Array.toList |> DetermineBaseDirectory
    match scaffoldingDirectory with
    | Some dir ->
        PrintVersionInformation ()
        printfn "Scaffolding to %s" dir
        StartBuild dir
    | None -> ()
    0
