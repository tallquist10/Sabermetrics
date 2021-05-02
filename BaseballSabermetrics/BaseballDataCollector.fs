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

    module NewAPI =
        open Sabermetrics.Dependencies.NewHtmlHandler
        let tryGetPlayerPage url (playerId:string) = 
            getSite (sprintf "%s/%c/%s.shtml" url (playerId.ToCharArray().[0]) playerId)

        let tryGetPlayerName playerId (doc:HtmlDocument) =
            let nodes = tryGetHtmlSections "#info #meta h1 > span" doc
            match nodes with
            | Result.Ok []-> Result.Error (FailedWebResponseParseException (sprintf "Could not find the name for the player with ID %s" playerId))
            | Result.Error e -> Result.Error e
            | Result.Ok (head::_) -> Result.Ok(doc,head)

        let tryCreatePlayer playerId (doc:HtmlDocument,nameElement:HtmlNode) = 
            let name = HtmlNodeExtensions.InnerText nameElement
            let stats = doc |> tryGetHtmlSections "#pitching_standard"
            match stats with
            | Result.Ok x -> Result.Ok (createPitcher name playerId)
            | Result.Error _ ->
                let stats = doc |> tryGetHtmlSections "#batting_standard" 
                match stats with
                | Result.Ok _ -> Result.Ok (createHitter name playerId)
                | Result.Error _ -> Result.Error (FailedDomainTranslationException (sprintf "Could not create player %s" name))

        let tryGatherPlayerStats doc player = 
            match player with
            | Hitter h -> doc |> (tryGetHtmlSections "#batting_standard tfoot tr")
            | Pitcher p -> doc |> (tryGetHtmlSections "#pitching_standard tfoot tr")

        let tryGetPlayerStats (doc:HtmlDocument) datasource player =
            let updateStatsForPlayer (stats: string list) player =
                match player with
                | Hitter _ -> Result.Ok (updateStats stats datasource player)
                | Pitcher _ -> Result.Ok (updateStats [] datasource player)

            let playerWithStats = 
                let statResult = tryGatherPlayerStats doc player
                match statResult with
                | Result.Ok statRow ->
                    let careerRow = statRow |> List.head
                    let careerStats = careerRow |> (fun n -> CssSelectorExtensions.CssSelect (n,"td"))
                    let careerNumbers = careerStats |> List.map (fun html -> 
                        HtmlNodeExtensions.InnerText html)
                    let convertedStats = careerNumbers
                    updateStatsForPlayer convertedStats player
                    
                | Result.Error e -> Result.Error e
            playerWithStats

        let tryInsertPlayerIntoDatabase (da:IPlayerDataAccess) player =
            da.InsertPlayerStats player
            |> Result.bind (fun _ -> Result.Ok player)

        let tryGetPlayerStatsFromDatabase (da:IPlayerDataAccess) player =
            match player with
            | Hitter hitter -> da.GetStatsForHitter (PlayerID hitter.ID)
            | Pitcher pitcher -> da.GetStatsForPitcher (PlayerID pitcher.ID)

        let getPlayerStats url da datasource playerId =
            let doc = tryGetPlayerPage url playerId
            match doc with
            | Result.Ok webpage ->
                webpage
                |> tryGetPlayerName playerId
                |> Result.bind (tryCreatePlayer playerId)
                |> Result.bind (tryGetPlayerStats webpage datasource)
                |> Result.bind (tryInsertPlayerIntoDatabase da)
                |> Result.bind (tryGetPlayerStatsFromDatabase da)
            | Result.Error e -> Result.Error e


    module OldAPI =
        let getPlayerPage url player =
            match player with
            | Result.Ok (Hitter h) -> getSite (sprintf "%s/%s" url h.ID)
            | Result.Ok (Pitcher p) -> getSite (sprintf "%s/%s" url p.ID)
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

        let gatherStats url (player:Result<Player,Exception>) =
            let doc = getPlayerPage url player
            match player with
            | Result.Ok (Hitter h) -> doc |> (getHtmlSections "#batting_standard tfoot tr")
            | Result.Ok (Pitcher p) -> doc |> (getHtmlSections "#pitching_standard tfoot tr")
            | Result.Error e -> Result.Error e

        let playerExistsinDatabase (da:PlayerDataAccess.IPlayerDataAccess) player =
            match player with
            | Result.Ok (Hitter hitter) -> 
                    match da.PlayerExists (PlayerID hitter.ID) with
                    | Result.Ok res -> res |> Option.ofObj |> Option.isSome
                    | Result.Error _ -> false
            | Result.Ok (Pitcher _) -> false 
            | Result.Error _ -> false

        let getStats url player =
            let updateStatsForPlayer (stats: string list) datasource player =
                match player with
                | Hitter _ -> Result.Ok (updateStats stats datasource player)
                | Pitcher _ -> Result.Ok (updateStats [] datasource player)

            let playerWithStats = 
                let statResult = gatherStats url player
                match statResult with 
                | Result.Ok statHtmls -> 
                    let careerRow = statHtmls |> List.head
                    let careerStats = careerRow |> (fun n -> CssSelectorExtensions.CssSelect (n,"td"))
                    let careerNumbers = careerStats |> List.map (fun html -> 
                        HtmlNodeExtensions.InnerText html)
                    let convertedStats = careerNumbers
                    player |> Result.bind (updateStatsForPlayer convertedStats Website)
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
