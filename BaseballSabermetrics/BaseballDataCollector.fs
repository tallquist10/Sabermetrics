namespace Sabermetrics.Baseball

open PlayerDataAccess

module BaseballDataCollector =
    open System
    open Sabermetrics.Exceptions
    open Sabermetrics.Dependencies.HtmlHandler
    open Sabermetrics.Dependencies.WebWorker
    open Stats
    open FSharp.Data
    open Domain

    let getPlayerPage url player = 
       match player with
       | Result.Ok p ->
        match p with
           | Hitter h -> getSite (sprintf "%s/%s" url h.Page)
           | Pitcher pitch -> getSite (sprintf "%s/%s" url pitch.Page)

       | Result.Error e -> Result.Error e

    let createPlayer (node: HtmlNode) =
        let name = HtmlNodeExtensions.InnerText node
        let page = HtmlNodeExtensions.AttributeValue (node, "href")
        let doc = getSite (sprintf "https://www.baseball-reference.com%s" page)
        let stats = doc |> getHtmlSections "#pitching_standard"
        match stats with
        | Result.Ok x -> Result.Ok (createPitcher name page)
        | Result.Error _ ->
            let stats = doc |> getHtmlSections "#batting_standard" 
            match stats with
            | Result.Ok _ -> Result.Ok (createHitter name page)
            | Result.Error _ -> Result.Error (FailedDomainTranslationException (sprintf "Could not create player %s" name))

    let isHitter player =
        match player with
        | Hitter h -> Result.Ok h
        | Pitcher _ -> Result.Error "Pitcher"

    let gatherStats url player =
        let doc = getPlayerPage url player
        match player with
        | Result.Ok (Hitter _) -> doc |> (getHtmlSections "#batting_standard tfoot tr")
        | Result.Ok (Pitcher _) -> doc |> (getHtmlSections "#pitching_standard tfoot tr")
        | Result.Error e -> Result.Error e

    let playerExistsinDatabase (da:PlayerDataAccess.IPlayerDataAccess) player =
        match player with
        | Result.Ok (Hitter hitter) -> 
                match da.PlayerExists (PlayerID hitter.Page) with
                | Result.Ok res -> res |> Option.ofObj |> Option.isSome
                | Result.Error _ -> false
        | Result.Ok (Pitcher _) -> false 
        | Result.Error _ -> false

    let getStats url player =
        let updateStatsForPlayer (stats: string list) player =
            match player with
            | Hitter _ -> Result.Ok (updateStats stats player)
            | Pitcher _ -> Result.Ok (updateStats [] player)

        let playerWithStats = 
            let statResult = gatherStats url player
            match statResult with 
            | Result.Ok statHtmls -> 
                let careerRow = statHtmls |> List.head
                let careerStats = careerRow |> (fun n -> CssSelectorExtensions.CssSelect (n,"td"))
                let careerNumbers = careerStats |> List.map (fun html -> 
                    HtmlNodeExtensions.InnerText html)
                let convertedStats = careerNumbers
                player |> Result.bind (updateStatsForPlayer convertedStats)
            | Result.Error e -> Result.Error (FailedWebRequestException (sprintf "Unable to get stats from web"))
        playerWithStats

    let getOkResults results: Result<'a, 'b> [] =
        results
        |> Array.filter (fun res -> match res with | Result.Error _ -> false | Result.Ok _ -> true)
        
    let getPlayersForLetterPage url letter =
        try
            let players = getSite (sprintf "%s/players/%c/" url letter)
            match players with
            | Result.Ok html -> Result.Ok {Html = html; Letter = letter}
            | Result.Error _ -> Result.Error (FailedWebRequestException (sprintf "Could not load the webpage for players whose last names start with '%c'" (Char.ToUpper letter)))
        with
        | ex -> Result.Error (FailedWebRequestException (sprintf "Could not load the webpage for players whose last names start with '%c'" (Char.ToUpper letter)))
            
    let getPlayersForLetter (letterPage: Result<LetterPage, Exception>) =
        match letterPage with
        | Result.Ok playersPage ->
            try
                let playerList = playersPage.Html |> Result.Ok |> getHtmlSections "#div_players_ p a"
                playerList
            with
            | e -> Result.Error (FailedWebRequestException (sprintf "Failed to extract list of players for letter '%c'" (Char.ToUpper playersPage.Letter)))
        | Result.Error e -> Result.Error e

    let getAllPlayers url letters =
        try
            let l = 
                letters
                |> Array.map (getPlayersForLetterPage url)
                |> Array.map getPlayersForLetter
                |> getOkResults
                |> Array.map (fun res -> match res with | Result.Ok v -> v |> List.toArray | Result.Error e -> [||])
                |> Array.collect id
            
            Result.Ok l

        with
        | _ -> Result.Error (FailedWebRequestException "Failed to combine results from all letters")

    let collectStatsForPlayer url (dataAccess:IPlayerDataAccess) page =
        let player = page |> createPlayer
        if not (playerExistsinDatabase dataAccess player) then
            player
            |> getStats url
            |> Result.bind dataAccess.InsertPlayerStats    
        else
            Result.Error PlayerAlreadyInDatabase

    let insertPlayerIntoDatabase (dataAccess: IPlayerDataAccess) player =
        dataAccess.InsertPlayerStats player


    let getStatsForPlayers url dataAccess players =
        match players with 
        | Result.Ok pages -> 
            pages
            |> Array.map (fun page -> collectStatsForPlayer url dataAccess page)
            |> getOkResults

        | Result.Error e -> [| Result.Error e |]
