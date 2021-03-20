namespace Sabermetrics
module Stats =
    open Domain  
    
    let ToInt stat =
        let str = stat |> string
        if str.Length = 0 then Stat 0 else
            stat |> string |> int |> Stat
    let ToFloat stat = 
        let str = stat |> string
        if str.Length = 0 then Stat 0.0 else
            stat |> string |> float |> Stat
 
    let createHitter name page = 
        Hitter {
            Name = name
            Page = page
            G = 0<G>
            PA = 0<PA>
            AB = 0<AB>
            R = 0<R>
            H = 0<H>
            Doubles = 0<Double>
            Triples = 0<Triple>
            HR = 0<HR>
            RBI = 0<RBI>
            SB = 0<SB>
            CS = 0<CS>
            BB = 0<BB>
            SO = 0<SO>
            BA = 0.0<BA>
            OBP = 0.0<OBP>
            SLG = 0.0<SLG>
            OPS = 0.0<OPS>
            OPSPlus = 0.0<OPSPlus>
            TB = 0<TB>
            GDP = 0<GDP>
            HBP = 0<HBP>
            SH = 0<SH>
            SF = 0<SF>
            IBB = 0<IBB>
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
    
    let addGames (games: string) player = 
        match player with
        | Hitter h -> Hitter {h with G = games |> int |> (*) 1<G>}
        | Pitcher p -> Pitcher {p with G = games |> ToInt}
    let addPA (pa: string) player =
        match player with
        | Hitter h -> Hitter {h with PA = pa |> int |> (*) 1<PA>}
        | _ -> player
    let addAB (ab: string) player = 
        match player with
        | Hitter h -> Hitter {h with AB = ab |> int |> (*) 1<AB>}
        | _ -> player
    let addRuns (r: string) player = 
        match player with
        | Hitter h -> Hitter {h with R = r |> int |> (*) 1<R>}
        | _ -> player
    let addHits (h: string) player = 
        match player with
        | Hitter hitter -> Hitter {hitter with H = h |> int |> (*) 1<H>}
        | _ -> player
    let add2B (db: string) player = 
        match player with
        | Hitter h -> Hitter {h with Doubles = db |> int |> (*) 1<Double>}
        | _ -> player
    let add3B (tp: string) player = 
        match player with
        | Hitter h -> Hitter {h with Triples = tp |> int |> (*) 1<Triple>}
        | _ -> player
    let addHR (hr: string) player = 
        match player with
        | Hitter h -> Hitter {h with HR = hr |> int |> (*) 1<HR>}
        | _ -> player
    let addRBI (rbi: string) player = 
        match player with
        | Hitter h -> Hitter {h with RBI = rbi |> int |> (*) 1<RBI>}
        | _ -> player
    let addSB (sb: string) player = 
        match player with
        | Hitter h -> Hitter {h with SB = sb |> int |> (*) 1<SB>}
        | _ -> player
    let addCS (cs: string) player = 
        match player with
        | Hitter h -> Hitter {h with CS = cs |> int |> (*) 1<CS>}
        | _ -> player
    let addBB (bb: string) player = 
        match player with
        | Hitter h -> Hitter {h with BB = bb |> int |> (*) 1<BB>}
        | _ -> player
    let addSO (so: string) player = 
        match player with
        | Hitter h -> Hitter {h with SO = so |> int |> (*) 1<SO>}
        | _ -> player
    let addBA (ba: string) player = 
        match player with
        | Hitter h -> Hitter {h with BA = ba |> float |> (*) 1.0<BA>}
        | _ -> player
    let addOBP (obp: string) player = 
        match player with
        | Hitter h -> Hitter {h with OBP = obp |> float |> (*) 1.0<OBP>}
        | _ -> player
    let addSLG (slg: string) player = 
        match player with
        | Hitter h -> Hitter {h with SLG = slg |> float |> (*) 1.0<SLG>}
        | _ -> player
    let addOPS (ops: string) player = 
        match player with
        | Hitter h -> Hitter {h with OPS = ops |> float |> (*) 1.0<OPS>}
        | _ -> player
    let addOPSplus (ops: string) player = 
        match player with
        | Hitter h -> Hitter {h with OPSPlus = ops |> float |> (*) 1.0<OPSPlus>}
        | _ -> player
    let addTB (tb: string) player = 
        match player with
        | Hitter h -> Hitter {h with TB = tb |> int |> (*) 1<TB>}
        | _ -> player
    let addGDP (gdp: string) player = 
        match player with
        | Hitter h -> Hitter {h with GDP = gdp |> int |> (*) 1<GDP>}
        | _ -> player
    let addHBP (hbp: string) player = 
        match player with
        | Hitter h -> Hitter {h with HBP = hbp |> int |> (*) 1<HBP>}
        | _ -> player
    let addSH (sh: string) player = 
        match player with
        | Hitter h -> Hitter {h with SH = sh |> int |> (*) 1<SH>}
        | _ -> player
    let addSF (sf: string) player = 
        match player with
        | Hitter h -> Hitter {h with SF = sf |> int |> (*) 1<SF>}
        | _ -> player
    let addIBB (ibb: string) player = 
        match player with
        | Hitter h -> Hitter {h with IBB = ibb |> int |> (*) 1<IBB>}
        | _ -> player
    

module BaseballDataCollector =
    open Sabermetrics.HtmlHandler
    open Sabermetrics.WebWorker
    open Stats
    open FSharp.Data
    open System
    open Domain

    let updateStats (stats: string list) player =
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

    let playerExistsinDatabase (da:PlayerDataAccess.IPlayerDataAccess) player =
        match player with
        | Result.Ok p ->
            match p with
            | Hitter hitter -> 
                match da.PlayerExists (PlayerID hitter.Page) with
                | Result.Ok res -> res |> Option.ofObj |> Option.isSome
                | Result.Error _ -> false
            | Pitcher _ -> false 
        | Result.Error _ -> false
        

    let getStats player =
        let updateStats player (stats: string list)  =
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
                let convertedStats = careerNumbers
                convertedStats |> updateStats player
            | Result.Error e -> Result.Error e
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
                |> Array.Parallel.map getPlayersForLetterPage
                |> Array.Parallel.map getPlayersForLetter
                |> getOkResults
                |> Array.Parallel.map (fun res -> match res with | Result.Ok v -> v |> List.toArray | Result.Error e -> [||])
                |> Array.collect id
            
            Result.Ok l

        with
        | _ -> Result.Error "Failed to combine results from all letters"

    let getStatsForPlayers players =
        let dataAccess = PlayerDataAccess.GetInstance "players.db"
        match players with 
        | Result.Ok pages -> 
            pages
            |> Array.Parallel.map createPlayer
            |> Array.filter (playerExistsinDatabase dataAccess)
            |> Array.Parallel.map getStats
            |> Array.Parallel.map (fun p -> Result.bind (isHitter) p)
            |> getOkResults

        | Result.Error e -> [| Result.Error "Could not retrieve stats for all players" |]
