namespace Sabermetrics.API

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.HttpsPolicy;
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open Sabermetrics

type Startup private () =
    new (configuration: IConfiguration) as this =
        Startup() then
        this.Configuration <- configuration 

    member this.ConfigureServices(services: IServiceCollection) = // This method gets called by the runtime. Use this method to add services to the container.
        // Add framework services.
        services.AddControllers() |> ignore
        // Register the Swagger generator, defining 1 or more Swagger documents
        services.AddSwaggerGen() |> ignore
        services.AddSingleton<Baseball.PlayerDataAccess.IPlayerDataAccess>(Baseball.PlayerDataAccess.GetInstance "baseball.db") |> ignore
        services.AddSingleton<Basketball.PlayerDataAccess.IPlayerDataAccess>(Basketball.PlayerDataAccess.GetInstance "basketball.db") |> ignore

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    member this.Configure(app: IApplicationBuilder, env: IWebHostEnvironment) =
        app.UseSwagger() |> ignore
        app.UseSwaggerUI(
            fun c -> 
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1")
                c.RoutePrefix <- "api"
        ) |> ignore
        if (env.IsDevelopment()) then
            app.UseDeveloperExceptionPage() |> ignore

        app.UseHttpsRedirection() |> ignore
        app.UseRouting() |> ignore

        app.UseAuthorization() |> ignore

        app.UseEndpoints(fun endpoints ->
            endpoints.MapControllers() |> ignore
            ) |> ignore

    member val Configuration : IConfiguration = null with get, set