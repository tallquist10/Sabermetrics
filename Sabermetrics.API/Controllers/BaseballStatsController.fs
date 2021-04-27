namespace Sabermetrics.API.Controllers

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open Sabermetrics.Baseball.Domain
open BaseballSabermetrics.API
open Microsoft.AspNetCore.Http

[<ApiController>]
[<Route("api/[controller]")>]
type BaseballStatsController (logger : ILogger<BaseballStatsController>) =
    inherit ControllerBase()

    [<HttpGet("{id}")>]
    [<ProducesResponseType(StatusCodes.Status200OK)>]
    [<ProducesResponseType(StatusCodes.Status404NotFound)>]
    member this.GetStatsForPlayer(PlayerID id) : IActionResult =
        let rng = System.Random()
        OkObjectResult([|
            for index in 0..4 ->
                { Date = DateTime.Now.AddDays(float index)
                  TemperatureC = rng.Next(-20,55)
                  Summary = "Steezy"}
        |]) :> IActionResult
