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
open Sabermetrics.Baseball.BaseballDataCollector
open Sabermetrics.Exceptions
open Microsoft.Extensions.Configuration

[<ApiController>]
[<Route("api/[controller]")>]
type BaseballStatsController (logger : ILogger<BaseballStatsController>, playerDataAccess:IPlayerDataAccess, config:IConfiguration) =
    inherit ControllerBase()

    member this.Configuration = config
    member this.Url = this.Configuration.["StatsWebsites:Baseball"]
    member this.PlayerDataAccess = playerDataAccess

    ///<summary>
    ///Get stats for a player from baseballreference.com
    ///</summary>
    [<HttpGet("{id}")>]
    [<ProducesResponseType(StatusCodes.Status200OK)>]
    [<ProducesResponseType(StatusCodes.Status404NotFound)>]
    member this.GetStatsForPlayer (id:string):IActionResult=
        let res = 
            getPlayersForLetterPage this.Url 'a'
            |> getPlayersForLetter
        match res with
        | Result.Ok nodes -> OkResult() :> IActionResult
        | Result.Error e -> BadRequestResult() :> IActionResult
