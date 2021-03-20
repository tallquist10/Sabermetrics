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

    type Exception =
    | SqliteException of string
    | PlayerNotFoundException
    | FailedWebRequestException of string