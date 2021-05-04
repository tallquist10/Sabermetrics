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
        PTS: float<PF>
    }