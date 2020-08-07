namespace Sabermetrics
module Stats =
    type Stat<'a> = Stat of 'a    
    
    let ToInt stat = 
        match stat with 
        | Stat s -> 
            let str = s |> string
            if str.Length = 0 then Stat 0 else
                s |> string |> int |> Stat
    let ToFloat stat = 
        match stat with 
        | Stat s -> 
            let str = s |> string
            if str.Length = 0 then Stat 0.0 else
                s |> string |> float |> Stat

    type Hitter = {
        Name: string
        Page: string
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

    type Pitcher = {
        Name: string
        Page: string
        Wins: Stat<int>
        Losses: Stat<int>
        WinPercent: Stat<float>
        ERA: Stat<float>
        G: Stat<int>
        GS: Stat<int>
        GF: Stat<int>
        CG: Stat<int>
        SHO: Stat<int>
        SV: Stat<int>
        IP: Stat<int>
        H: Stat<int>
        R: Stat<int>
        ER: Stat<int>
        HR: Stat<int>
        BB: Stat<int>
        IBB: Stat<int>
        SO: Stat<int>
        HBP: Stat<int>
        BK: Stat<int>
        WP: Stat<int>
        BF: Stat<int>
        ERAplus: Stat<int>
        FIP: Stat<float>
        WHIP: Stat<float>
        H9: Stat<float>
        HR9: Stat<float>
        BB9: Stat<float>
        SO9: Stat<float>
        SOperW: Stat<float>
    }

    type Player = 
    | Hitter of Hitter
    | Pitcher of Pitcher
 
    let createHitter name page = 
        Hitter {
            Name = name
            Page = page
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

    let createPitcher name page = //TODO: Change pitching stats
        Pitcher {
            Name = name
            Page = page
            Wins = Stat 0
            Losses = Stat 0
            WinPercent = Stat 0.0
            ERA = Stat 0.0
            G = Stat 0
            GS = Stat 0
            GF = Stat 0
            CG = Stat 0
            SHO = Stat 0
            SV = Stat 0
            IP = Stat 0
            H = Stat 0
            R = Stat 0
            ER = Stat 0
            HR = Stat 0
            BB = Stat 0
            IBB = Stat 0
            SO = Stat 0
            HBP = Stat 0
            BK = Stat 0
            WP = Stat 0
            BF = Stat 0
            ERAplus = Stat 0
            FIP = Stat 0.0
            WHIP = Stat 0.0
            H9 = Stat 0.0
            HR9 = Stat 0.0
            BB9 = Stat 0.0
            SO9 = Stat 0.0
            SOperW = Stat 0.0
        }
    
    let addGames (games: Stat<string>) player = 
        match player with
        | Hitter h -> Hitter {h with G = games |> ToInt}
        | Pitcher p -> Pitcher {p with G = games |> ToInt}
    let addPA (pa: Stat<string>) player =
        match player with
        | Hitter h -> Hitter {h with PA = pa |> ToInt}
        | _ -> player
    let addAB (ab: Stat<string>) player = 
        match player with
        | Hitter h -> Hitter {h with AB = ab |> ToInt}
        | _ -> player
    let addRuns (r: Stat<string>) player = 
        match player with
        | Hitter h -> Hitter {h with R = r |> ToInt}
        | _ -> player
    let addHits (h: Stat<string>) player = 
        match player with
        | Hitter hitter -> Hitter {hitter with H = h |> ToInt}
        | _ -> player
    let add2B (db: Stat<string>) player = 
        match player with
        | Hitter h -> Hitter {h with Doubles = db |> ToInt}
        | _ -> player
    let add3B (tp: Stat<string>) player = 
        match player with
        | Hitter h -> Hitter {h with Triples = tp |> ToInt}
        | _ -> player
    let addHR (hr: Stat<string>) player = 
        match player with
        | Hitter h -> Hitter {h with HR = hr |> ToInt}
        | _ -> player
    let addRBI (rbi: Stat<string>) player = 
        match player with
        | Hitter h -> Hitter {h with RBI = rbi |> ToInt}
        | _ -> player
    let addSB (sb: Stat<string>) player = 
        match player with
        | Hitter h -> Hitter {h with SB = sb |> ToInt}
        | _ -> player
    let addCS (cs: Stat<string>) player = 
        match player with
        | Hitter h -> Hitter {h with CS = cs |> ToInt}
        | _ -> player
    let addBB (bb: Stat<string>) player = 
        match player with
        | Hitter h -> Hitter {h with BB = bb |> ToInt}
        | _ -> player
    let addSO (so: Stat<string>) player = 
        match player with
        | Hitter h -> Hitter {h with SO = so |> ToInt}
        | _ -> player
    let addBA (ba: Stat<string>) player = 
        match player with
        | Hitter h -> Hitter {h with BA = ba |> ToFloat}
        | _ -> player
    let addOBP (obp: Stat<string>) player = 
        match player with
        | Hitter h -> Hitter {h with OBP = obp |> ToFloat}
        | _ -> player
    let addSLG (slg: Stat<string>) player = 
        match player with
        | Hitter h -> Hitter {h with SLG = slg |> ToFloat}
        | _ -> player
    let addOPS (ops: Stat<string>) player = 
        match player with
        | Hitter h -> Hitter {h with OPS = ops |> ToFloat}
        | _ -> player
    let addOPSplus (ops: Stat<string>) player = 
        match player with
        | Hitter h -> Hitter {h with OPSPlus = ops |> ToFloat}
        | _ -> player
    let addTB (tb: Stat<string>) player = 
        match player with
        | Hitter h -> Hitter {h with TB = tb |> ToInt}
        | _ -> player
    let addGDP (gdp: Stat<string>) player = 
        match player with
        | Hitter h -> Hitter {h with GDP = gdp |> ToInt}
        | _ -> player
    let addHBP (hbp: Stat<string>) player = 
        match player with
        | Hitter h -> Hitter {h with HBP = hbp |> ToInt}
        | _ -> player
    let addSH (sh: Stat<string>) player = 
        match player with
        | Hitter h -> Hitter {h with SH = sh |> ToInt}
        | _ -> player
    let addSF (sf: Stat<string>) player = 
        match player with
        | Hitter h -> Hitter {h with SF = sf |> ToInt}
        | _ -> player
    let addIBB (ibb: Stat<string>) player = 
        match player with
        | Hitter h -> Hitter {h with IBB = ibb |> ToInt}
        | _ -> player
    

module BaseballDataCollector =
    open Sabermetrics.HtmlHandler
    open Sabermetrics.WebWorker
    open Stats
    open FSharp.Data
    open System
        

    let updateStats (stats: Stat<string> list) player =
        // Functions for adding stats to a player
        match player with
        | Hitter _ -> 
            (player
            |> addGames (stats.[0])
            |> addPA (stats.[1])
            |> addAB (stats.[2])
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
            |> addIBB (stats.[23]))

        | Pitcher _ -> player

   
     
    let getPlayerPage player = 
       match player with
       | Result.Ok p ->
        match p with
           | Hitter h -> getSite (sprintf "https://www.baseball-reference.com%s" h.Page)
           | Pitcher pitch -> getSite (sprintf "https://www.baseball-reference.com%s" pitch.Page)

       | Result.Error e -> Result.Error e

    let createPlayer (node: HtmlNode) =
        let name = HtmlNodeExtensions.InnerText node
        let page = HtmlNodeExtensions.AttributeValue (node, "href")
        let doc = getSite (sprintf "https://www.baseball-reference.com%s" page)
        let stats = doc |> getHtmlSections "#pitching_standard"
        match stats with
        | Result.Ok x -> Result.Ok (createPitcher name page)
        | Result.Error e ->
            let stats = doc |> getHtmlSections "#batting_standard" 
            match stats with
            | Result.Ok _ -> Result.Ok (createHitter name page)
            | Result.Error e -> Result.Error (sprintf "Could not create player %s" name)

    let isHitter player =
        match player with
        | Hitter h -> Result.Ok h
        | Pitcher _ -> Result.Error "Pitcher"

    let gatherStats player =
        let doc = getPlayerPage player
        match player with
        | Result.Ok p ->
            match p with
            | Hitter p -> doc |> getHtmlSections "#batting_standard tfoot tr"
            | Pitcher p -> doc |> getHtmlSections "#pitching_standard tfoot tr"
        | Result.Error e -> Result.Error e

    let getStats player =
        let updateStats player (stats: Stat<string> list)  =
            match player with
            | Result.Ok p ->
                match p with
                | Hitter _ -> Result.Ok (updateStats stats p)
                | Pitcher _ -> Result.Ok (updateStats [] p)

            | Result.Error e -> Result.Error e

        let playerWithStats = 
            let statResult = gatherStats player
            match statResult with 
            | Result.Ok statHtmls -> 
                let careerRow = statHtmls |> List.head
                let careerStats = careerRow |> (fun n -> CssSelectorExtensions.CssSelect (n,"td"))
                let careerNumbers = careerStats |> List.map (fun html -> 
                    HtmlNodeExtensions.InnerText html)
                let convertedStats = careerNumbers |> List.map (fun s -> Stat s)
                convertedStats |> updateStats player
            | Result.Error e -> Result.Error e
        
        match playerWithStats with
        | Result.Ok player -> printfn "Stats have been updated for %s" (match player with | Hitter h -> h.Name | Pitcher p -> p.Name)
        | Result.Error _ -> ()
        playerWithStats

    let getOkResults results: Result<'a, 'b> [] =
        results
        |> Array.filter (fun res -> match res with | Result.Error _ -> false | Result.Ok _ -> true)
        
    let getPlayersForLetterPage letter =
        try
            let players = getSite (sprintf "https://baseball-reference.com/players/%c/" letter)
            match players with
            | Result.Ok html -> Result.Ok {Html = html; Letter = letter}
            | Result.Error _ -> Result.Error (sprintf "Could not load the webpage for players whose last names start with '%c'" (Char.ToUpper letter))
        with
        | ex -> Result.Error (sprintf "Could not load the webpage for players whose last names start with '%c'" (Char.ToUpper letter))
            
    let getPlayersForLetter (letterPage: Result<LetterPage, string>) =
        match letterPage with
        | Result.Ok playersPage ->
            try
                let playerList = playersPage.Html |> Result.Ok |> getHtmlSections "#div_players_ p a"
                playerList
            with
            | ex -> Result.Error (sprintf "Failed to extract list of players for letter '%c'" (Char.ToUpper playersPage.Letter))
        | Result.Error msg -> Result.Error msg

   

    let getAllPlayers letters =
        try
            let l = 
                letters
                |> Array.map getPlayersForLetterPage
                |> Array.map getPlayersForLetter
                |> getOkResults
                |> Array.map (fun res -> match res with | Result.Ok v -> v |> List.toArray | Result.Error e -> [||])
                |> Array.collect id
            
            Result.Ok l

        with
        | _ -> Result.Error "Failed to combine results from all letters"

    let getStatsForPlayers players =
        match players with 
        | Result.Ok pages -> 
            pages
            |> Array.map (createPlayer >> getStats)
            |> Array.map (fun p -> Result.bind (isHitter) p)
            |> Result.Ok

        | Result.Error e -> Result.Error "Could not retrieve stats for all players"
