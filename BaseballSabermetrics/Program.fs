open Sabermetrics.WebWorker
open Sabermetrics.HtmlHandler
open Sabermetrics.BaseballDataCollector
[<EntryPoint>]
let main argv =
    let players = getAllPlayers ['a'..'z']
    //match getStatsForPlayers (getAllPlayers ['a'..'z']) with
    match players with
    | Result.Ok players -> printfn "%i" players.Length
    | Result.Error e -> printfn "Welp... looks like that didn't work"
    0 // return an integer exit code
