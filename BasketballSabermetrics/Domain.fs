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
        MP: int<MP>
        FG: int<FG>
        FGA: int<FGA>
        Threes: int<Threes>
        ThreesAttempted: int<ThreesAttempted>
        Twos: int<Twos>
        TwosAttempted: int<TwosAttempted>
        FT: int<FT>
        FTA: int<FTA>
        ORB: int<ORB>
        DRB: int<DRB>
        AST: int<AST>
        STL: int<STL>
        BLK: int<BLK>
        TO: int<TO>
        PF: int<PF>
        PTS: int<PTS>
    }

    type DataSource =
        | Website
        | Database

    let createPlayer name id = {
        Name = name
        ID = id
        G = 0<G>
        GS = 0<GS>
        MP = 0<MP>
        FG = 0<FG>
        FGA = 0<FGA>
        Threes = 0<Threes>
        ThreesAttempted = 0<ThreesAttempted>
        Twos = 0<Twos>
        TwosAttempted = 0<TwosAttempted>
        FT = 0<FT>
        FTA = 0<FTA>
        ORB = 0<ORB>
        DRB = 0<DRB>
        AST = 0<AST>
        STL = 0<STL>
        BLK = 0<BLK>
        TO = 0<TO>
        PF = 0<PF>
        PTS = 0<PTS>
    }

module Stats =
    open Domain
    let addGames (g:string) player =
        {player with G = g |> int |> (*) 1<G> }

    let addGS (gs: string) player =
        { player with GS = gs |> int |> (*) 1<GS> }

    let addMP (mp: string) player =
        { player with MP = mp |> int |> (*) 1<MP>}
       
    let addFG (fg: string) player =
         { player with FG = fg |> int |> (*) 1<FG>}

    let addFGA (fga: string) player =
        { player with FGA = fga |> int |> (*) 1<FGA>}

    let add3P (threes: string) player =
        { player with Threes = threes |> int |> (*) 1<Threes>}

    let add3PA (threesAttempted: string) player =
        { player with ThreesAttempted = threesAttempted |> int |> (*) 1<ThreesAttempted>}

    let add2P (twos: string) player =
        { player with Twos = twos |> int |> (*) 1<Twos>}

    let add2PA (twosAttempted: string) player =
        { player with TwosAttempted = twosAttempted |> int |> (*) 1<TwosAttempted>}

    let addFT (ft: string) player =
        { player with FT = ft |> int |> (*) 1<FT>}

    let addFTA (fta: string) player =
        { player with FTA = fta |> int |> (*) 1<FTA>}

    let addORB (orb: string) player =
        { player with ORB = orb |> int |> (*) 1<ORB>}

    let addDRB (drb: string) player =
        { player with DRB = drb |> int |> (*) 1<DRB>}

    let addAST (ast: string) player =
        { player with AST = ast |> int |> (*) 1<AST>}

    let addSTL (stl: string) player =
        { player with STL = stl |> int |> (*) 1<STL>}

    let addBLK (blk: string) player =
        { player with BLK = blk |> int |> (*) 1<BLK>}

    let addTO (tos: string) player =
        { player with TO = tos |> int |> (*) 1<TO>}

    let addPF (pf: string) player =
        { player with PF = pf |> int |> (*) 1<PF>}

    let addPTS (pts: string) player =
        { player with PTS = pts |> int |> (*) 1<PTS>}

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
                |> addGames (stats.[4] |> formatStatString)
                |> addGS (stats.[5] |> formatStatString)
                |> addMP (stats.[6] |> formatStatString)
                |> addFG (stats.[7] |> formatStatString)
                |> addFGA (stats.[8] |> formatStatString)
                |> add3P (stats.[10] |> formatStatString)
                |> add3PA (stats.[11] |> formatStatString)
                |> add2P (stats.[13] |> formatStatString)
                |> add2PA (stats.[14] |> formatStatString)
                |> addFT (stats.[17] |> formatStatString)
                |> addFTA (stats.[18] |> formatStatString)
                |> addORB (stats.[20] |> formatStatString)
                |> addDRB (stats.[21] |> formatStatString)
                |> addAST (stats.[23] |> formatStatString)
                |> addSTL (stats.[24] |> formatStatString)
                |> addBLK (stats.[25] |> formatStatString)
                |> addTO (stats.[26] |> formatStatString)
                |> addPF (stats.[27] |> formatStatString)
                |> addPTS (stats.[28] |> formatStatString)
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
                
