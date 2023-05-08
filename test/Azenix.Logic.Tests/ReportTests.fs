module Azenix.Logic.Tests.ReportTests

open FsUnit.Xunit
open Xunit
open Azenix.Logic.Report
open Azenix.Logic.LogEntry

[<Fact>]
let ``Distinct IP addresses removes duplicates`` () =
    let logs = seq {
                    { IP = "111.222.333.444"; Url = "/nothing" };
                    { IP = "111.222.333.555"; Url = "/nothing" };
                    { IP = "111.222.333.666"; Url = "/nothing" };
                    { IP = "111.222.333.444"; Url = "/nothing" };
                }
    let expected = 3
    logs |> numberUniqueIPAddresses |> should equal expected

[<Fact>]
let ``Top 3 unique IP addresses selects 3 top 3 including ties`` () =
    let logs = seq {
                    { IP = "111.222.333.444"; Url = "/nothing" };
                    { IP = "111.222.333.555"; Url = "/nothing" };
                    { IP = "111.222.333.666"; Url = "/nothing" };
                    { IP = "111.222.333.444"; Url = "/nothing" };
                    { IP = "111.222.333.555"; Url = "/nothing" };
                    { IP = "111.222.333.666"; Url = "/nothing" };
                    { IP = "111.222.333.777"; Url = "/nothing" };
                }
    let expected = seq { "111.222.333.444"; "111.222.333.555"; "111.222.333.666" }
    logs |> top3MostActiveIPs |> Seq.compareWith Operators.compare expected |> should equal 0
    
[<Fact>]
let ``Top 3 unique IP addresses handles ties deterministically`` () =
    let logs = seq {
                    { IP = "111.222.333.444"; Url = "/nothing" };
                    { IP = "111.222.333.555"; Url = "/nothing" };
                    { IP = "111.222.333.666"; Url = "/nothing" }
                    { IP = "111.222.333.777"; Url = "/nothing" };
                    { IP = "111.222.333.444"; Url = "/nothing" };
                    { IP = "111.222.333.555"; Url = "/nothing" };
                    { IP = "111.222.333.666"; Url = "/nothing" };
                    { IP = "111.222.333.777"; Url = "/nothing" };
                }
    let expected = seq { "111.222.333.444"; "111.222.333.555"; "111.222.333.666" }
    logs |> top3MostActiveIPs |> Seq.compareWith Operators.compare expected |> should equal 0
    
[<Fact>]
let ``Top 3 unique IP addresses handles fewer than 3 records`` () =
    let logs = seq {
                    { IP = "111.222.333.444"; Url = "/nothing" };
                    { IP = "111.222.333.555"; Url = "/nothing" };
                }
    let expected = seq { "111.222.333.444"; "111.222.333.555"; }
    logs |> top3MostActiveIPs |> Seq.compareWith Operators.compare expected |> should equal 0
    
[<Fact>]
let ``Top 3 active URLs selects 3 top 3 including ties`` () =
    let logs = seq {
                    { IP = "111.222.333.444"; Url = "/a" };
                    { IP = "111.222.333.555"; Url = "/b" };
                    { IP = "111.222.333.666"; Url = "/c" };
                    { IP = "111.222.333.444"; Url = "/a" };
                    { IP = "111.222.333.555"; Url = "/b" };
                    { IP = "111.222.333.666"; Url = "/c" };
                    { IP = "111.222.333.777"; Url = "/d" };
                }
    let expected = seq { "/a"; "/b"; "/c" }
    logs |> top3MostVisitedUrls |> Seq.compareWith Operators.compare expected |> should equal 0
    
[<Fact>]
let ``Top 3 active URLs handles ties deterministically`` () =
    let logs = seq {
                    { IP = "111.222.333.444"; Url = "/a" };
                    { IP = "111.222.333.555"; Url = "/b" };
                    { IP = "111.222.333.666"; Url = "/c" }
                    { IP = "111.222.333.777"; Url = "/d" };
                    { IP = "111.222.333.444"; Url = "/a" };
                    { IP = "111.222.333.555"; Url = "/b" };
                    { IP = "111.222.333.666"; Url = "/c" };
                    { IP = "111.222.333.777"; Url = "/d" };
                }
    let expected = seq { "/a"; "/b"; "/c" }
    logs |> top3MostVisitedUrls |> Seq.compareWith Operators.compare expected |> should equal 0
    
[<Fact>]
let ``Top 3 active URLs handles fewer than 3 records`` () =
    let logs = seq {
                    { IP = "111.222.333.444"; Url = "/a" };
                    { IP = "111.222.333.555"; Url = "/b" };
                }
    let expected = seq { "/a"; "/b"; }
    logs |> top3MostVisitedUrls |> Seq.compareWith Operators.compare expected |> should equal 0