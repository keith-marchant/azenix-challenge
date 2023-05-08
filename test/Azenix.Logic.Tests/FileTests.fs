module Azenix.Logic.Tests.FileTests

open System
open Xunit
open FsUnit.Xunit
open Azenix.Logic.File

[<Fact>]
let ``Loading valid file creates line sequence`` () =
    let results = readLocalFile @".\example-data.log"
    match results with
    | Ok lines -> lines |> Seq.length |> should equal 23
    | Error _ -> false |> should equal true
    
[<Fact>]
let ``Loading missing directory throws error`` () =
    let results = readLocalFile @"E:\directory_doesnt_exist.txt"
    match results with
    | Ok _ -> false |> should equal true
    | Error ex -> Assert.IsAssignableFrom<System.IO.DirectoryNotFoundException>(ex) |> ignore

[<Fact>]
let ``Loading missing file throws error`` () =
    let results = readLocalFile @"..\file_doesnt_exist.txt"
    match results with
    | Ok _ -> false |> should equal true
    | Error ex -> Assert.IsAssignableFrom<System.IO.FileNotFoundException>(ex) |> ignore