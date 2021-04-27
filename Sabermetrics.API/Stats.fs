namespace BaseballSabermetrics.API

open System

type Stats =
    { Date: DateTime
      TemperatureC: int
      Summary: string }

    member this.TemperatureF =
        32 + (int (float this.TemperatureC / 0.5556))
