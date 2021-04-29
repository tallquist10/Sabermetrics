namespace Sabermetrics.Baseball
module Domain =
    open System.Net
    open FSharp.Data

    [<Measure>] type G
    [<Measure>] type PA
    [<Measure>] type AB
    [<Measure>] type R
    [<Measure>] type H
    [<Measure>] type Double
    [<Measure>] type Triple
    [<Measure>] type HR
    [<Measure>] type RBI
    [<Measure>] type SB
    [<Measure>] type CS
    [<Measure>] type BB
    [<Measure>] type SO
    [<Measure>] type BA
    [<Measure>] type OBP
    [<Measure>] type SLG
    [<Measure>] type OPS
    [<Measure>] type OPSPlus
    [<Measure>] type TB
    [<Measure>] type GDP
    [<Measure>] type HBP
    [<Measure>] type SH
    [<Measure>] type SF
    [<Measure>] type IBB


    type PlayerID = PlayerID of string
    type PlayerPage = PlayerPage of HtmlDocument
    type LetterPage = {Html: HtmlDocument; Letter: char}

    type Stat<'a> = Stat of 'a  
    type Hitter = {
        Name: string
        Page: string
        G: int<G>
        PA: int<PA>
        AB: int<AB>
        R: int<R>
        H: int<H>
        Doubles: int<Double>
        Triples: int<Triple>
        HR: int<HR>
        RBI: int<RBI>
        SB: int<SB>
        CS: int<CS>
        BB: int<BB>
        SO: int<SO>
        BA: float<BA>
        OBP: float<OBP>
        SLG: float<SLG>
        OPS: float<OPS>
        OPSPlus: float<OPSPlus>
        TB: int<TB>
        GDP: int<GDP>
        HBP: int<HBP>
        SH: int<SH>
        SF: int<SF>
        IBB: int<IBB>
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
        
    let formatStatString stat = 
        match stat with
        | null
        | "" -> "0"
        | _ -> stat

    let updateStats (stats: string list) player =
        match stats with
        | [] -> player
        | _ ->
        // Functions for adding stats to a player
            match player with
            | Hitter _ -> 
                (player
                |> addGames (stats.[0] |> formatStatString)
                |> addPA (stats.[1] |> formatStatString)
                |> addAB (stats.[2] |> formatStatString)
                |> addRuns (stats.[3] |> formatStatString)
                |> addHits (stats.[4] |> formatStatString)
                |> add2B (stats.[5] |> formatStatString)
                |> add3B (stats.[6] |> formatStatString)
                |> addHR (stats.[7] |> formatStatString)
                |> addRBI (stats.[8] |> formatStatString)
                |> addSB (stats.[9] |> formatStatString)
                |> addCS (stats.[10] |> formatStatString)
                |> addBB (stats.[11] |> formatStatString)
                |> addSO (stats.[12] |> formatStatString)
                |> addBA (stats.[13] |> formatStatString)
                |> addOBP (stats.[14] |> formatStatString)
                |> addSLG (stats.[15] |> formatStatString)
                |> addOPS (stats.[16] |> formatStatString)
                |> addOPSplus (stats.[17] |> formatStatString)
                |> addTB (stats.[18] |> formatStatString)
                |> addGDP (stats.[19] |> formatStatString)
                |> addHBP (stats.[20] |> formatStatString)
                |> addSH (stats.[21] |> formatStatString)
                |> addSF (stats.[22] |> formatStatString)
                |> addIBB (stats.[23] |> formatStatString)
                )
            | Pitcher _ -> player
