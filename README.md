## Azenix Challenge

### Getting Started

1. Install .NET 7
2. Clone this repo
3. Execute `dotnet restore` in the repository root
4. Execute the tests `dotnet test`
    - Verify that all 13 tests pass
5. Navigate to the executable directory `cd .\src\Azenix.Runner`
6. Execute the console application `dotnet run`

### Description

This is an F# sample of the Azenix interview test written to demonstrate the conciseness and 
flexibility of the F# language. The logic for the application is separated into its 3 concerns,
1. Reading the input
2. Parsing the input
3. Reporting the output

It should be straight forward to create new parsers or readers as formats change from the provided exmaples.


