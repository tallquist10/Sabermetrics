namespace Sabermetrics
module PlayerDataTypes =
    type Player = {
        Name: string
        Page: string
    } 

module BaseballDataCollector =
    open Sabermetrics.HtmlHandler
    open Sabermetrics.WebWorker
    open PlayerDataTypes

    let parseHtml html =
        match html with
        | Some text -> text
        | None -> "No fun allowed"

    let getPlayersForLetter letter =
        let players = 
            getSite (sprintf "https://baseball-reference.com/players/%s/" letter)
            |> wrapResonse
            |> handleResponse
            |> parseHtml
        let playerList = players |> getHtmlSectionByID "div" "div_players_" |> getHtmlSections "p"
        let pList = playerList |> List.map (
                        fun playerSection -> 
                            let name = getInnerContent "a" playerSection
                            let link = getLinkText playerSection
                            {
                                Name = name
                                Page = link
                            }
                    )
        players

    let getAllPlayers =
        ['a'..'z']
        |> List.map string
        |> List.map (fun letter -> letter, (getPlayersForLetter letter))
        |> Map.ofList

        
        
        
