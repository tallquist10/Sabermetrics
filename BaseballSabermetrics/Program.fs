open Sabermetrics.DataCollection
[<EntryPoint>]
let main argv =
    let html = getBaseballSite |> wrapResonse |> handleResponse
    match html with
    | Some text -> printfn "%s" text
    | None -> printfn "No resulting text. Sorry :/"
    printfn "Hello World from F#!"
    0 // return an integer exit code
