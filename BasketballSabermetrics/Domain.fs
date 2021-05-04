namespace Sabermetrics.Basketball
module Domain =
    open FSharp.Data
    [<Measure>] type G
    [<Measure>] type GS
    [<Measure>] type MP
    [<Measure>] type FG
    [<Measure>] type FGA
    [<Measure>] type Threes
    [<Measure>] type ThreesAttempted
    [<Measure>] type Twos
    [<Measure>] type TwosAttempted
    [<Measure>] type FT
    [<Measure>] type FTA
    [<Measure>] type ORB
    [<Measure>] type DRB
    [<Measure>] type AST
    [<Measure>] type STL
    [<Measure>] type BLK
    [<Measure>] type TO
    [<Measure>] type PF
    [<Measure>] type PTS

    type PlayerID = PlayerID of string
    type PlayerPage = PlayerPage of HtmlDocument
    type LetterPage = {Html: HtmlDocument; Letter: char}

    type Player = {
        Name: string
        ID: string
        G: int<G>
        GS: int<GS>
        MP: float<MP>
        FG: float<FG>
        FGA: float<FGA>
        Threes: float<Threes>
        ThreesAttempted: float<ThreesAttempted>
        Twos: float<Twos>
        TwosAttempted: float<TwosAttempted>
        FT: float<FT>
        FTA: float<FTA>
        ORB: float<ORB>
        DRB: float<DRB>
        AST: float<AST>
        STL: float<STL>
        BLK: float<BLK>
        TO: float<TO>
        PF: float<PF>
        PTS: float<PTS>
    }

    type DataSource =
        | Website
        | Database

    let createPlayer name id = {
        Name = name
        ID = id
        G = 0<G>
        GS = 0<GS>
        MP = 0.0<MP>
        FG = 0.0<FG>
        FGA = 0.0<FGA>
        Threes = 0.0<Threes>
        ThreesAttempted = 0.0<ThreesAttempted>
        Twos = 0.0<Twos>
        TwosAttempted = 0.0<TwosAttempted>
        FT = 0.0<FT>
        FTA = 0.0<FTA>
        ORB = 0.0<ORB>
        DRB = 0.0<DRB>
        AST = 0.0<AST>
        STL = 0.0<STL>
        BLK = 0.0<BLK>
        TO = 0.0<TO>
        PF = 0.0<PF>
        PTS = 0.0<PTS>
    }

module Stats =
    open Domain
    let addGames (g:string) player =
        {player with G = g |> int |> (*) 1<G> }

    let addGS (gs: string) player =
        { player with GS = gs |> int |> (*) 1<GS> }

    let addMP (mp: string) player =
        { player with MP = mp |> float |> (*) 1.0<MP>}
       
    let addFG (fg: string) player =
         { player with FG = fg |> float |> (*) 1.0<FG>}

    let addFGA (fga: string) player =
        { player with FGA = fga |> float |> (*) 1.0<FGA>}

    let add3P (threes: string) player =
        { player with Threes = threes |> float |> (*) 1.0<Threes>}

    let add3PA (threesAttempted: string) player =
        { player with ThreesAttempted = threesAttempted |> float |> (*) 1.0<ThreesAttempted>}

    let add2P (twos: string) player =
        { player with Twos = twos |> float |> (*) 1.0<Twos>}

    let add2PA (twosAttempted: string) player =
        { player with TwosAttempted = twosAttempted |> float |> (*) 1.0<TwosAttempted>}

    let addFT (ft: string) player =
        { player with FT = ft |> float |> (*) 1.0<FT>}

    let addFTA (fta: string) player =
        { player with FTA = fta |> float |> (*) 1.0<FTA>}

    let addORB (orb: string) player =
        { player with ORB = orb |> float |> (*) 1.0<ORB>}

    let addDRB (drb: string) player =
        { player with DRB = drb |> float |> (*) 1.0<DRB>}

    let addAST (ast: string) player =
        { player with AST = ast |> float |> (*) 1.0<AST>}

    let addSTL (stl: string) player =
        { player with STL = stl |> float |> (*) 1.0<STL>}

    let addBLK (blk: string) player =
        { player with BLK = blk |> float |> (*) 1.0<BLK>}

    let addTO (tos: string) player =
        { player with TO = tos |> float |> (*) 1.0<TO>}

    let addPF (pf: string) player =
        { player with PF = pf |> float |> (*) 1.0<PF>}

    let addPTS (pts: string) player =
        { player with PTS = pts |> float |> (*) 1.0<PTS>}

    let formatStatString stat = 
        match stat with
        | null
        | "" -> "0"
        | _ -> stat

    let updateStats (stats: string list) datasource player =
        match stats with
        | [] -> player
        | _ ->
        // Functions for adding stats to a player
            match datasource with
            | Website ->
                player
                |> addGames (stats.[0] |> formatStatString)
                |> addGS (stats.[1] |> formatStatString)
                |> addMP (stats.[2] |> formatStatString)
                |> addFG (stats.[3] |> formatStatString)
                |> addFGA (stats.[4] |> formatStatString)
                |> add3P (stats.[6] |> formatStatString)
                |> add3PA (stats.[7] |> formatStatString)
                |> add2P (stats.[9] |> formatStatString)
                |> add2PA (stats.[10] |> formatStatString)
                |> addFT (stats.[13] |> formatStatString)
                |> addFTA (stats.[14] |> formatStatString)
                |> addORB (stats.[16] |> formatStatString)
                |> addDRB (stats.[17] |> formatStatString)
                |> addAST (stats.[18] |> formatStatString)
                |> addSTL (stats.[19] |> formatStatString)
                |> addBLK (stats.[20] |> formatStatString)
                |> addTO (stats.[21] |> formatStatString)
                |> addPF (stats.[22] |> formatStatString)
                |> addPTS (stats.[23] |> formatStatString)
            | Database ->
                player
                |> addGames (stats.[0] |> formatStatString)
                |> addGS (stats.[1] |> formatStatString)
                |> addMP (stats.[2] |> formatStatString)
                |> addFG (stats.[3] |> formatStatString)
                |> addFGA (stats.[4] |> formatStatString)
                |> add3P (stats.[5] |> formatStatString)
                |> add3PA (stats.[6] |> formatStatString)
                |> add2P (stats.[7] |> formatStatString)
                |> add2PA (stats.[8] |> formatStatString)
                |> addFT (stats.[9] |> formatStatString)
                |> addFTA (stats.[10] |> formatStatString)
                |> addORB (stats.[11] |> formatStatString)
                |> addDRB (stats.[12] |> formatStatString)
                |> addAST (stats.[13] |> formatStatString)
                |> addSTL (stats.[14] |> formatStatString)
                |> addBLK (stats.[15] |> formatStatString)
                |> addTO (stats.[16] |> formatStatString)
                |> addPF (stats.[17] |> formatStatString)
                |> addPTS (stats.[18] |> formatStatString)
                
