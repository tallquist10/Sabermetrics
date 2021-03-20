namespace Sabermetrics
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

    type Exception =
    | SqliteException of string
    | PlayerNotFoundException
    | FailedWebRequestException of string