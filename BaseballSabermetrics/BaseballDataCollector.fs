namespace Sabermetrics
module Player =
    type Player = {
        Name: string
        Page: string
        G: int
        PA: int
        AB: int
        R: int
        H: int
        Doubles: int
        Triples: int
        HR: int
        RBI: int
        SB: int
        CS: int
        BB: int
        SO: int
        BA: float
        OBP: float
        SLG: float
        OPS: float
        OPSPlus: float
        TB: int
        GDP: int
        HBP: int
        SH: int
        SF: int
        IBB: int
    } 

    let create name page = 
        {
            Name = name
            Page = page
            G = 0
            PA = 0
            AB = 0
            R = 0
            H = 0
            Doubles = 0
            Triples = 0
            HR = 0
            RBI = 0
            SB = 0
            CS = 0
            BB = 0
            SO = 0
            BA = 0.0 
            OBP = 0.0 
            SLG = 0.0 
            OPS = 0.0
            OPSPlus = 0.0
            TB = 0
            GDP = 0
            HBP = 0
            SH = 0
            SF = 0
            IBB = 0
        }
    
    // Functions for adding stats to a player

    let addGames games player = {player with G = games}
    let addPA pa player = {player with PA = pa}
    let addAB ab player = {player with AB = ab}
    let addRuns r player = {player with R = r}
    let addHits h player = {player with H = h}
    let add2B db player = {player with Doubles = db}
    let add3B tp player = {player with Triples = tp}
    let addHR hr player = {player with HR = hr}
    let addRBI rbi player = {player with RBI = rbi}
    let addSB sb player = {player with SB = sb}
    let addCS cs player = {player with CS = cs}
    let addBB bb player = {player with BB = bb}
    let addSO so player = {player with SO = so}
    let addBA ba player = {player with BA = ba}
    let addOBP obp player = {player with OBP = obp}
    let addSLG slg player = {player with SLG = slg}
    let addOPS ops player = {player with OPS = ops}
    let addOPSplus ops player = {player with OPSPlus = ops}
    let addTB tb player = {player with TB = tb}
    let addGDP gdp player = {player with GDP = gdp}
    let addHBP hbp player = {player with HBP = hbp}
    let addSH sh player = {player with SH = sh}
    let addSF sf player = {player with SF = sf}
    let addIBB ibb player = {player with IBB = ibb}

module BaseballDataCollector =
    open Sabermetrics.HtmlHandler
    open Sabermetrics.WebWorker
    open Player
    open FSharp.Data

    let parseHtml html =
        match html with
        | Some text -> text
        | None -> "No fun allowed"

    let createPlayer (node: HtmlNode) =
        Player.create (HtmlNodeExtensions.InnerText node) (HtmlNodeExtensions.AttributeValue (node, "href"))

    let updateStats (stats: string list) player =
        player
        |> addGames (int stats.[0])
        |> addPA (int stats.[1])
        |> addAB (int stats.[2])
        |> addRuns (int  stats.[3])
        |> addHits (int stats.[4])
        |> add2B (int stats.[5])
        |> add3B (int stats.[6])
        |> addHR (int stats.[7])
        |> addRBI (int stats.[8])
        |> addSB (int stats.[9])
        |> addCS (int stats.[10])
        |> addBB (int stats.[11])
        |> addSO (int stats.[12])
        |> addBA (float stats.[13])
        |> addOBP (float stats.[14])
        |> addSLG (float stats.[15])
        |> addOPS (float stats.[16])
        |> addOPSplus (float stats.[17])
        |> addTB (int stats.[18])
        |> addGDP (int stats.[19])
        |> addHBP (int stats.[20])
        |> addSH (int stats.[21])
        |> addSF (int stats.[22])
        |> addIBB (int stats.[23])

    let getStats player =
        let stats = 
            let doc = getSite (sprintf "https://baseball-reference.com%s" player.Page) 
            doc
            |> getHtmlSections "#div_batting_standard tfoot tr:first-child td"
            |> List.map (fun html -> html.InnerText)
            |> List.map string
        let update = updateStats stats player
            
        update

    let getPlayersForLetter letter =
        let players = 
            getSite (sprintf "https://baseball-reference.com/players/%s/" letter)
        let playerList = players |> getHtmlSections "#div_players_ p a"
        let pList = playerList |> List.map (fun playerSection -> createPlayer playerSection) 
        pList

    let getAllPlayers =
        let l = 
            ['a'..'z']
            |> List.map string
            |> List.map (fun letter -> (getPlayersForLetter letter))
            |> List.collect (fun list -> list)
        l

    let getStatsForAllPlayers =
        getAllPlayers |> List.map (fun player -> getStats player)
        


        
        
        
