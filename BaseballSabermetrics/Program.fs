open Sabermetrics.WebWorker
open Sabermetrics.HtmlHandler
open Sabermetrics.BaseballDataCollector
[<EntryPoint>]
let main argv =
    match getStatsForPlayers (getAllPlayers ['a'..'z']) with
    | Result.Ok players -> printfn "%A" players
    | Result.Error e -> printfn "Welp... looks like that didn't work"
    0 // return an integer exit code
