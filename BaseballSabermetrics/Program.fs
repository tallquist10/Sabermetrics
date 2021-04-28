//open Sabermetrics.WebWorker
//open Sabermetrics.HtmlHandler
//open Sabermetrics.Stats
//open Sabermetrics
//open BaseballDataCollector
//open Domain


//let dataAccess = PlayerDataAccess.GetInstance "baseball.db"

//let getStatsForSabermetrics (players:Result<Player,Exception> []) =
//    players
//    |> getOkResults
//    |> Array.map (function | Ok h -> h)

[<EntryPoint>]
let main argv =
    //printfn "Retrieving Baseball stats from baseball-reference.com. This will likely take 2+ hours..."
    //[|'a'..'z'|]
    //|> getAllPlayers
    //|> getStatsForPlayers dataAccess
    //|> getStatsForSabermetrics
    //|> Array.map (insertPlayerIntoDatabase dataAccess)
    //|> getOkResults
    //|> (function
    //    | list when list.Length > 1 -> printfn "Process complete. You may now exit this window."
    //    | _ -> printfn "Process failed"
    //)
    
    0 // return an integer exit code
