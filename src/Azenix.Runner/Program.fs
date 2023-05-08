open Azenix.Logic.File
open Azenix.Logic.Parser
open Azenix.Logic.Report

let parsedData = match @"./example-data.log" |> readLocalFile with
                 | Ok data -> data |> parseCLF |> Seq.toList // ToList materialises the sequence so we don't run into issues with a closed stream later
                 | Error ex -> ex.Message |> failwith

printfn "Number of unique IP addresses"
parsedData |> numberUniqueIPAddresses |> printfn "%d"

printfn "Top 3 most visited URLs"
parsedData |> top3MostVisitedUrls |> Seq.iter (fun x -> printfn $"%A{x}")

printfn "Top 3 most active IPs"
parsedData |> top3MostActiveIPs |> Seq.iter (fun x -> printfn $"%A{x}")
