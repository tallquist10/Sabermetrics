namespace Sabermetrics.API.Controllers

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open BaseballSabermetrics.API
open Microsoft.AspNetCore.Http
open Sabermetrics.Baseball.Domain
open Sabermetrics.Baseball.PlayerDataAccess
open Sabermetrics.Baseball.BaseballDataCollector.NewAPI
open Sabermetrics.Dependencies.HtmlHandler
open Sabermetrics.Exceptions
open Microsoft.Extensions.Configuration
open Sabermetrics

[<ApiController>]
[<Route("api/[controller]")>]
type BaseballStatsController (logger : ILogger<BaseballStatsController>, playerDataAccess:IPlayerDataAccess, config:IConfiguration) =
    inherit ControllerBase()

    member this.Configuration = config
    member this.Url = this.Configuration.["StatsWebsites:Baseball"]
    member this.PlayerDataAccess = playerDataAccess

    /// <summary>
    ///Get stats for a player from baseballreference.com
    /// </summary>
    [<HttpGet("{playerID}")>]
    [<ProducesResponseType(StatusCodes.Status200OK)>]
    [<ProducesResponseType(StatusCodes.Status404NotFound)>]
    member this.GetStatsForPlayer (playerID:string):IActionResult=
        let playerExists = this.PlayerDataAccess.PlayerExists (PlayerID playerID)
        match playerExists with
        | Result.Ok null
        | Error _ ->
            let res = getPlayerStats this.Url this.PlayerDataAccess Website playerID
            match res with
            | Result.Ok player -> OkObjectResult(player) :> IActionResult
            | Result.Error error -> BadRequestObjectResult(error) :> IActionResult
        | Result.Ok _ ->
            try
                this.PlayerDataAccess.GetStatsForHitter (PlayerID playerID)
                |> function
                | Result.Ok player -> OkObjectResult(player) :> IActionResult
                | Result.Error error -> BadRequestObjectResult(error) :> IActionResult
            with
            | error -> BadRequestObjectResult(error) :> IActionResult
        
