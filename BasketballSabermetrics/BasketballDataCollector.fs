namespace Sabermetrics.Basketball
open PlayerDataAccess

module BasketballDataCollector =
    open System
    open Sabermetrics.Exceptions
    open Sabermetrics.Dependencies.HtmlHandler
    open Sabermetrics.Dependencies.WebWorker
    open Stats
    open FSharp.Data
    open Domain

    module NewAPI =
        open Sabermetrics.Dependencies.NewHtmlHandler
        module SinglePlayer =
            let tryGetPlayerPage url (playerId:string) = 
                getSite (sprintf "%s/players/%c/%s.html" url (playerId.ToCharArray().[0]) playerId)

            let tryGetPlayerName playerId (doc:HtmlDocument) =
                let nodes = tryGetHtmlSections "#info #meta h1 > span" doc
                match nodes with
                | Result.Ok []-> Result.Error (FailedWebResponseParseException (sprintf "Could not find the name for the player with ID %s" playerId))
                | Result.Error e -> Result.Error e
                | Result.Ok (head::_) -> Result.Ok(doc,head)

            let tryCreatePlayer playerId (doc:HtmlDocument,nameElement:HtmlNode) = 
                let name = HtmlNodeExtensions.InnerText nameElement
                let stats = doc |> tryGetHtmlSections "#totals"
                match stats with
                | Result.Ok x -> Result.Ok (createPlayer name playerId)
                | Result.Error _ -> Result.Error (FailedDomainTranslationException (sprintf "Could not create player %s" name))

            let tryGatherPlayerStats doc player = doc |> (tryGetHtmlSections "#totals tfoot tr")

            let tryGetPlayerStats (doc:HtmlDocument) datasource player =
                let updateStatsForPlayer (stats: string list) player = Result.Ok (updateStats stats datasource player)

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

            let tryGetPlayerStatsFromDatabase (da:IPlayerDataAccess) player =  da.GetStatsForPlayer (PlayerID player.ID)                

            let getPlayerStatsFromWebsite url da playerId =
                let doc = tryGetPlayerPage url playerId
                match doc with
                | Result.Ok webpage ->
                    let player =
                        webpage
                        |> tryGetPlayerName playerId
                        |> Result.bind (tryCreatePlayer playerId)
                        |> Result.bind (tryGetPlayerStats webpage Website)
                    player 
                    |> Result.bind (tryInsertPlayerIntoDatabase da)
                    |> ignore

                    player
                | Result.Error e -> Result.Error e

            let getPlayerStatsFromDatabase da playerId =
                tryGetPlayerStatsFromDatabase da playerId

        module MultiplePlayers =
            open System.Text.RegularExpressions
            let tryGetLetterPage url letter =
                try
                    let players = getSite (sprintf "%s/players/%c/" url letter)
                    match players with
                    | Result.Ok html -> Result.Ok {Html = html; Letter = letter}
                    | Result.Error _ -> Result.Error (FailedWebRequestException (sprintf "Could not load the webpage for players whose last names start with '%c'" (Char.ToUpper letter)))
                with
                | ex -> Result.Error (FailedWebRequestException (sprintf "Could not load the webpage for players whose last names start with '%c'" (Char.ToUpper letter)))
            
            let tryGetPlayersForLetter letterPage =
                try
                    let playerList = letterPage.Html |> Result.Ok |> getHtmlSections "#div_players_ p a"
                    playerList
                with
                | e -> Result.Error (FailedWebRequestException (sprintf "Failed to extract list of players for letter '%c'" (Char.ToUpper letterPage.Letter)))

            let tryGetPlayerPageFromNode node =
                let name = HtmlNodeExtensions.InnerText node
                let page = HtmlNodeExtensions.AttributeValue (node, "href")
                getSite (sprintf "https://www.baseball-reference.com%s" page)

            let tryGetPlayerIDFromNode (node: HtmlNode) =
                let name = HtmlNodeExtensions.InnerText node
                let linkString = HtmlNodeExtensions.AttributeValue (node, "href")
                let getID = Regex("/players/[a-z]/(?<id>\w+).shtml")
                let playerIDMatch = getID.Match(linkString)
                let playerID = playerIDMatch.Groups.[1].ToString()
                playerID

            let tryGetPlayerStatsFromNode url da (node:HtmlNode) =
                node
                |> tryGetPlayerIDFromNode
                |> SinglePlayer.getPlayerStatsFromWebsite url da
                

            let getStatsForAllPlayersForLetter url da datasource letter =
                letter
                |> tryGetLetterPage url
                |> Result.bind tryGetPlayersForLetter
                |> function
                   | Result.Ok nodes -> List.map (tryGetPlayerStatsFromNode url da) nodes
                   | Result.Error error -> [Result.Error error]

