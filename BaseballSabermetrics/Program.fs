open Sabermetrics.WebWorker
open Sabermetrics.HtmlHandler
open Sabermetrics.BaseballDataCollector
[<EntryPoint>]
let main argv =
    printfn "%A" getAllPlayers
    0 // return an integer exit code
