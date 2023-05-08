module Azenix.Logic.File

open System.IO

type DataReader = string -> Result<string seq,exn>

let readLine (reader: StreamReader) =
    try
        reader.ReadLine() |> Some
    with ex ->
        None

let readLocalFile : DataReader =
    fun path ->      
        try
            let r = new StreamReader(File.OpenRead(path))

            seq {
                use reader = r
                let mutable error = false

                while not error && not reader.EndOfStream do
                    let nextLine = readLine reader
                    if nextLine.IsSome then yield nextLine.Value else error <- true
            } |> Ok
        with ex ->
            Error ex

