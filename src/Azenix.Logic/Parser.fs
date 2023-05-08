module Azenix.Logic.Parser

open System.Text.RegularExpressions
open Azenix.Logic.LogEntry

let CLFRegex = Regex("(?<ip>\S+) (?<logName>\S+) (?<user>\S+) (?<time>\[[^]]+\]) \"(?<verb>\S+) (?<url>\S+) (?<protocol>\S+) (?<status>\S+) (?<bytes>\S+)", RegexOptions.Compiled)

let parseLine (lineRegex: Regex) line =
    match lineRegex.Split(line) with
    | [| head; ip; logName; user; time; verb; url; protocol; status; bytes; tail; |] ->
        Some {
            IP = ip
            Url = url
        }
    | _ -> None

let parseCLFLine = parseLine CLFRegex

let parse lineParser data =
    data
    |> Seq.map lineParser
    |> Seq.choose id

let parseCLF: seq<string> -> seq<LogEntry> = parse parseCLFLine
