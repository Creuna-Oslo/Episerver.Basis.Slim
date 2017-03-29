module TemplateExtraction

open System
open System.IO
open System.Resources
open ICSharpCode.SharpZipLib.Zip
open ICSharpCode.SharpZipLib.Core
open System.Runtime.Serialization.Formatters.Binary

let GetTemplateObject () =
    let res = ResourceManager("Resources", Reflection.Assembly.GetExecutingAssembly())
    res.GetObject("Template")

let ConvertTemplateToStream template =
    let stream = new MemoryStream()

    let formatter = new BinaryFormatter()
    formatter.Serialize(stream, template)
    stream.Seek(int64 0, SeekOrigin.Begin) |> ignore
    stream
     
let UnzipFile target (stream : Stream)=

    let file = new ZipFile(stream)
    for entry in file do
        match entry with
        | :? ZipEntry as zipEntry -> 
            let unzipPath = Path.Combine(target, zipEntry.Name)
            let directoryPath = Path.GetDirectoryName(unzipPath)
            // create directory if needed
            if directoryPath.Length > 0 then Directory.CreateDirectory(directoryPath) |> ignore
            // unzip the file
            let zipStream = file.GetInputStream(zipEntry)
            let buffer = Array.create 32768 0uy
            if unzipPath.EndsWith "/" |> not then 
                use unzippedFileStream = File.Create(unzipPath)
                StreamUtils.Copy(zipStream, unzippedFileStream, buffer)
        | _ -> ()

let ExtractTemplate targetDirectory =
    GetTemplateObject ()
    |> ConvertTemplateToStream
    |> UnzipFile targetDirectory