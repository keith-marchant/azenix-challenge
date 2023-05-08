module Azenix.Logic.Report

open Azenix.Logic.LogEntry

let numberUniqueIPAddresses data =
    data
    |> Seq.map (fun x -> x.IP)
    |> Seq.distinct
    |> Seq.length
 
let aggregateByProperty propertySelector numToCount data =
    data
    |> Seq.countBy propertySelector
    |> Seq.sortByDescending snd
    |> Seq.truncate numToCount
    |> Seq.map fst
    
let mostVisitedUrls = aggregateByProperty (fun x -> x.Url)

let top3MostVisitedUrls: seq<LogEntry> -> seq<string> = mostVisitedUrls 3

let mostActiveIPs = aggregateByProperty (fun x -> x.IP)

let top3MostActiveIPs: seq<LogEntry> -> seq<string> = mostActiveIPs 3