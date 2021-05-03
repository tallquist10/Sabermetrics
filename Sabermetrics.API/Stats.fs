namespace BaseballSabermetrics.API

open System
open Sabermetrics.Baseball.Domain

[<Measure>] type Base
[<Measure>] type OnBase

type Stats =
    { 
    Name: string
    ID: string
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
    TB: int<TB>
    GDP: int<GDP>
    HBP: int<HBP>
    SH: int<SH>
    SF: int<SF>
    IBB: int<IBB> 
    }

    member this.BA =
        (this.H / this.AB) |> float

    member this.OBP = 
        ((((this.H * 1<OnBase/H> + this.BB * 1<OnBase/BB> + this.HBP * 1<OnBase/HBP>)) |> int) / this.PA) |> float

    member this.SLG =
        let homeruns = this.HR * 1<Base/HR>
        let triples = this.Triples * 1<Base/Triple>
        let doubles = this.Doubles * 1<Base/Double>
        let singles = this.H * 1<Base/H> - doubles - triples - homeruns //total hits minus the hits that are already accounted for elsewhere
        ((singles + doubles * 2 + triples * 3 + homeruns * 4) / this.AB) |> float

    member this.OPS =
        this.OBP + this.SLG

