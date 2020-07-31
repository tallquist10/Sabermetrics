namespace Sabermetrics
module Stats =
    type Stat<'a> = Stat of 'a
    type HitterStats = {
        G: Stat<int>
        PA: Stat<int>
        AB: Stat<int>
        R: Stat<int>
        H: Stat<int>
        Doubles: Stat<int>
        Triples: Stat<int>
        HR: Stat<int>
        RBI: Stat<int>
        SB: Stat<int>
        CS: Stat<int>
        BB: Stat<int>
        SO: Stat<int>
        BA: Stat<float>
        OBP: Stat<float>
        SLG: Stat<float>
        OPS: Stat<float>
        OPSPlus: Stat<float>
        TB: Stat<int>
        GDP: Stat<int>
        HBP: Stat<int>
        SH: Stat<int>
        SF: Stat<int>
        IBB: Stat<int>
    }

    type PitcherStats = Stats of string //placeholder for later

    type StatLine = 
    | HitterStats of HitterStats
    | PitcherStats of PitcherStats
    
    
    let ToInt (stat: Stat<'a>) = stat |> string |> int
    let ToFloat (stat: Stat<'a>) = stat |> string |> float   

    type Player = {
        Name: string
        Page: string
        Stats: StatLine
    }
 
  (*  let createHitter name page = 
        {
            Name = name
            Page = page
            Stats = HitterStats {
                G = Stat 0
                PA = Stat 0
                AB = Stat 0
                R = Stat 0
                H = Stat 0
                Doubles = Stat 0
                Triples = Stat 0
                HR = Stat 0
                RBI = Stat 0
                SB = Stat 0
                CS = Stat 0
                BB = Stat 0
                SO = Stat 0
                BA = Stat 0.0 
                OBP = Stat 0.0 
                SLG = Stat 0.0 
                OPS = Stat 0.0
                OPSPlus = Stat 0.0
                TB = Stat 0
                GDP = Stat 0
                HBP = Stat 0
                SH = Stat 0
                SF = Stat 0
                IBB = Stat 0
            }
        }

    let createPitcher name page = //TODO: Change pitching stats
    {
        Name = name
        Page = page
        Stats = HitterStats {
            G = Stat 0
            PA = Stat 0
            AB = Stat 0
            R = Stat 0
            H = Stat 0
            Doubles = Stat 0
            Triples = Stat 0
            HR = Stat 0
            RBI = Stat 0
            SB = Stat 0
            CS = Stat 0
            BB = Stat 0
            SO = Stat 0
            BA = Stat 0.0 
            OBP = Stat 0.0 
            SLG = Stat 0.0 
            OPS = Stat 0.0
            OPSPlus = Stat 0.0
            TB = Stat 0
            GDP = Stat 0
            HBP = Stat 0
            SH = Stat 0
            SF = Stat 0
            IBB = Stat 0
        }
    }
    
    let addGames (games: Stat<string>) player = {player with Stats = {HitterStats with G = games |> Stats.ToInt}}
    let addPA (pa: Stat<string>) player = {player with Stats = {HitterStats with PA = pa |> Stats.ToInt}}
    let addAB (ab: Stat<string>) player = {player with Stats = {HitterStats with AB = ab |> Stats.ToInt}}
    let addRuns (r: Stat<string>) player = {player with Stats = {HitterStats with R = r |> Stats.ToInt}}
    let addHits (h: Stat<string>) player = {player with Stats = {HitterStats with H = h |> Stats.ToInt}}
    let add2B (db: Stat<string>) player = {player with Stats = {HitterStats with Doubles = db |> Stats.ToInt}}
    let add3B (tp: Stat<string>) player = {player with Stats = {HitterStats with Triples = tp |> Stats.ToInt}}
    let addHR (hr: Stat<string>) player = {player with Stats = {HitterStats with HR = hr |> Stats.ToInt}}
    let addRBI (rbi: Stat<string>) player = {player with Stats = {HitterStats with RBI = rbi |> Stats.ToInt}}
    let addSB (sb: Stat<string>) player = {player with Stats = {HitterStats with SB = sb |> Stats.ToInt}}
    let addCS (cs: Stat<string>) player = {player with Stats = {HitterStats with CS = cs |> Stats.ToInt}}
    let addBB (bb: Stat<string>) player = {player with Stats = {HitterStats with BB = bb |> Stats.ToInt}}
    let addSO (so: Stat<string>) player = {player with Stats = {HitterStats with SO = so |> Stats.ToInt}}
    let addBA (ba: Stat<string>) player = {player with Stats = {HitterStats with BA = ba |> Stats.ToFloat}}
    let addOBP (obp: Stat<string>) player = {player with Stats = {HitterStats with OBP = obp |> Stats.ToFloat}}
    let addSLG (slg: Stat<string>) player = {player with Stats = {HitterStats with SLG = slg |> Stats.ToFloat}}
    let addOPS (ops: Stat<string>) player = {player with Stats = {HitterStats with OPS = ops |> Stats.ToFloat}}
    let addOPSplus (ops: Stat<string>) player = {player with Stats = {HitterStats with OPSPlus = ops |> Stats.ToFloat}}
    let addTB (tb: Stat<string>) player = {player with Stats = {HitterStats with TB = tb |> Stats.ToInt}}
    let addGDP (gdp: Stat<string>) player = {player with Stats = {HitterStats with GDP = gdp |> Stats.ToInt}}
    let addHBP (hbp: Stat<string>) player = {player with Stats = {HitterStats with HBP = hbp |> Stats.ToInt}}
    let addSH (sh: Stat<string>) player = {player with Stats = {HitterStats with SH = sh |> Stats.ToInt}}
    let addSF (sf: Stat<string>) player = {player with Stats = {HitterStats with SF = sf |> Stats.ToInt}}
    let addIBB (ibb: Stat<string>) player = {player with Stats = {HitterStats with IBB = ibb |> Stats.ToInt}}

    type Position =
    | Hitter of Player
    | Pitcher of Player*)
    

module BaseballDataCollector =
    open Sabermetrics.HtmlHandler
    open Sabermetrics.WebWorker
    open Stats
    open FSharp.Data
    open System

    (*let parseHtml html =
        match html with
        | Some text -> text
        | None -> "No fun allowed"*)

    (*let getPlayerPage player = 
        getSite (sprintf "https://www.baseball-reference.com/players%s" player.Page)*)
(*
    let positionForPlayer player =
        let doc = getPlayerPage player
        let result = doc |> getHtmlSections "#pitching_standard"
        match result with
        | _::_ -> Pitcher player
        | _ -> Hitter player*)

   (* let createPlayer (node: HtmlNode) =
        BaseballTypes.createHitter 
            (HtmlNodeExtensions.InnerText node) 
            (HtmlNodeExtensions.AttributeValue (node, "href"))*)

(*    let updateHitterStats (stats: list<Stat<string>>) player =
        // Functions for adding stats to a player
        player
        |> addGames (stats.[0])
        |> addPA (stats.[1])
        |> addAB ((stats.[2])
        |> addRuns (stats.[3])
        |> addHits (stats.[4])
        |> add2B (stats.[5])
        |> add3B (stats.[6])
        |> addHR (stats.[7])
        |> addRBI (stats.[8])
        |> addSB (stats.[9])
        |> addCS (stats.[10])
        |> addBB (stats.[11])
        |> addSO (stats.[12])
        |> addBA (stats.[13])
        |> addOBP (stats.[14])
        |> addSLG (stats.[15])
        |> addOPS (stats.[16])
        |> addOPSplus (stats.[17])
        |> addTB (stats.[18])
        |> addGDP (stats.[19])
        |> addHBP (stats.[20])
        |> addSH (stats.[21])
        |> addSF (stats.[22])
        |> addIBB (stats.[23])

    let updatePitcherStats player =
        player

    let gatherStats player =
        let doc = getPlayerPage player
        let position = positionForPlayer player
        match position with
        | Hitter p -> doc |> getHtmlSections "#pitching_standard tfoot tr:first-child td"
        | Pitcher p -> doc |> getHtmlSections "#batting_standard tfoot tr:first-child td"

    let updateStats (stats: list<Stat<string>>) player =
        let position = positionForPlayer player
        match position with
        | Hitter h -> updateHitterStats stats player
        | Pitcher p -> updatePitcherStats player

    let getStats player =
        let stats = 
            let stats = gatherStats player
            stats
            |> List.map (fun html -> html.InnerText)
            |> List.map Stat
        let update = updateStats stats player
            
        update*)

    let getPlayersForLetterPage letter =
        try
            let players = getSite (sprintf "https://baseball-reference.com/players/%c/" letter)
            Result.Ok {Html = players; Letter = letter}
        with
        | ex -> Result.Error (sprintf "Could not load the webpage for players whose last names start with '%c'" (Char.ToUpper letter))
            
    let getPlayersForLetter (letterPage: Result<LetterPage, string>) =
        match letterPage with
        | Ok playersPage ->
            try
                let playerList = playersPage.Html |> getHtmlSections "#div_players_ p a"
                Result.Ok playerList
            with
            | ex -> Result.Error (sprintf "Failed to extract list of players for letter '%c'" (Char.ToUpper playersPage.Letter))
        | Error msg -> Result.Error msg

    let getAllPlayers =
        let l = 
            ['a'..'z']
            |> List.map getPlayersForLetterPage
            |> List.map getPlayersForLetter
            |> List.collect (fun list -> list)
        l

    let getStatsForAllPlayers =
        getAllPlayers |> List.map (fun player -> getStats player)