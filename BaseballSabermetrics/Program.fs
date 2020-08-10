open Sabermetrics.WebWorker
open Sabermetrics.HtmlHandler
open Sabermetrics.Stats
open Sabermetrics.BaseballDataCollector
open Sabermetrics.ExcelExport

let getStatsForSabermetrics (players:Result<Hitter,string> []) =
    match players with
       | list when list.Length >= 1 ->
           list
           |> Array.map (fun hitter -> 
                         match hitter with
                         | Result.Ok h -> h
                         )
           |> insertPlayersToCells
           
       | _ -> () 
[<EntryPoint>]
let main argv =
    printfn "Retrieving Baseball stats from baseball-reference.com. This will likely take 2+ hours..."
    getStatsForSabermetrics (getStatsForPlayers (getAllPlayers [|'a'..'z'|]))
    printfn "Process complete. You may now exit this window."
    0 // return an integer exit code
