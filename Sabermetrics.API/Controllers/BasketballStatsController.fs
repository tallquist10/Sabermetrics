namespace Sabermetrics.API.Controllers

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open BaseballSabermetrics.API
open Microsoft.AspNetCore.Http
open Sabermetrics.Basketball.Domain
open Sabermetrics.Basketball.PlayerDataAccess
open Sabermetrics.Basketball.BasketballDataCollector.NewAPI
open Sabermetrics.Dependencies.HtmlHandler
open Sabermetrics.Exceptions
open Microsoft.Extensions.Configuration
open Sabermetrics

[<ApiController>]
[<Route("api/[controller]")>]
type BasketballStatsController (logger : ILogger<BasketballStatsController>, playerDataAccess:IPlayerDataAccess, config:IConfiguration) =
    inherit ControllerBase()

    member this.Configuration = config
    member this.Url = this.Configuration.["StatsWebsites:Basketball"]
    member this.PlayerDataAccess = playerDataAccess

    /// <summary>
    ///Get stats for a player from basketballreference.com
    /// </summary>
    [<HttpGet("{playerID}")>]
    [<ProducesResponseType(StatusCodes.Status200OK)>]
    [<ProducesResponseType(StatusCodes.Status404NotFound)>]
    member this.GetStatsForPlayer (playerID:string):IActionResult =
        let playerExists = this.PlayerDataAccess.PlayerExists (PlayerID playerID)
        match playerExists with
        | Result.Ok null
        | Error _ ->
            let res = SinglePlayer.getPlayerStatsFromWebsite this.Url this.PlayerDataAccess playerID
            match res with
            | Result.Ok player -> OkObjectResult(player) :> IActionResult
            | Result.Error error -> BadRequestObjectResult(error) :> IActionResult
        | Result.Ok _ ->
            try
                this.PlayerDataAccess.GetStatsForPlayer (PlayerID playerID)
                |> function
                | Result.Ok player -> OkObjectResult(player) :> IActionResult
                | Result.Error error -> BadRequestObjectResult(error) :> IActionResult
            with
            | error -> BadRequestObjectResult(error) :> IActionResult

    /// <summary>
    ///Get stats for all players from basketballreference.com
    /// </summary>
    [<HttpGet("all")>]
    [<ProducesResponseType(StatusCodes.Status200OK)>]
    [<ProducesResponseType(StatusCodes.Status404NotFound)>]
    member this.StoreStatsForAllPlayers():IActionResult =
        try
            for letter in 'a'..'z' do
                MultiplePlayers.getStatsForAllPlayersForLetter this.Url this.PlayerDataAccess Website letter
                |> ignore
            OkResult() :> IActionResult 
        with
        | e -> (BadRequestObjectResult(e) :> IActionResult)
        

