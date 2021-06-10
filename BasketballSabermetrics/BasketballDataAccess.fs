namespace Sabermetrics.Basketball
open Microsoft.Data.Sqlite
open Domain
open Stats
open Sabermetrics.Exceptions
open Sabermetrics.Dependencies.SqliteHelpers

[<RequireQualifiedAccess>]
module SqlCommands =
    [<Literal>]
    let PlayerExists = """
    SELECT 1 FROM Players
    WHERE ID = @ID
    LIMIT 1
    """
    [<Literal>]
    let InsertPlayerStats = """
    INSERT INTO Players (ID, Name, G, GS, MP, FG, FGA, Threes, ThreesAttempted, Twos, 
    TwosAttempted, FT, FTA, ORB, DRB, AST, STL, BLK, Turnovers, PF, PTS)
    VALUES (
    @ID
    ,@Name
    ,@G
    ,@GS
    ,@MP
    ,@FG
    ,@FGA
    ,@Threes
    ,@ThreesAttempted
    ,@Twos
    ,@TwosAttempted
    ,@FT
    ,@FTA
    ,@ORB
    ,@DRB
    ,@AST
    ,@STL
    ,@BLK
    ,@TO
    ,@PF
    ,@PTS
    );
    """
    [<Literal>]
    let GetPlayerStats = """
    SELECT Name, G, GS, MP, FG, FGA, Threes, ThreesAttempted, Twos, 
    TwosAttempted, FT, FTA, ORB, DRB, AST, STL, BLK, Turnovers, PF, PTS
    FROM Players
    WHERE ID = @ID
    """

module PlayerDataAccess =
    open Sabermetrics.Dependencies
    type IPlayerDataAccess =
        abstract member Connection: SqliteConnection
        abstract member PlayerExists: PlayerID -> Result<obj,Exception>
        abstract member InsertPlayerStats: Player -> Result<unit,Exception>
        abstract member GetStatsForPlayer: PlayerID -> Result<Player,Exception>

    let private playerExists (connection:SqliteConnection) (PlayerID id) =
        try
            connection.CreateCommand()
            |> setText SqlCommands.PlayerExists
            |> addParameter "@ID" id
            |> executeScalar
            |> Result.Ok
        with
        | e -> Result.Error (SqliteException (sprintf "Unable to find player with id: %s" id))

    let private insertPlayerStats (connection: SqliteConnection) (player: Player) =
        try
            let command = connection.CreateCommand()
            command
            |> setText SqlCommands.InsertPlayerStats
            |> addParameter "@ID" player.ID
            |> addParameter "@Name" player.Name
            |> addParameter "@G" player.G
            |> addParameter "@GS" player.GS
            |> addParameter "@MP" player.MP
            |> addParameter "@FG" player.FG
            |> addParameter "@FGA" player.FGA
            |> addParameter "@Threes" player.Threes
            |> addParameter "@ThreesAttempted" player.ThreesAttempted
            |> addParameter "@Twos" player.Twos
            |> addParameter "@TwosAttempted" player.TwosAttempted
            |> addParameter "@FT" player.FT
            |> addParameter "@FTA" player.FTA
            |> addParameter "@ORB" player.ORB
            |> addParameter "@DRB" player.DRB
            |> addParameter "@AST" player.AST
            |> addParameter "@STL" player.STL
            |> addParameter "@BLK" player.BLK
            |> addParameter "@TO" player.TO
            |> addParameter "@PF" player.PF
            |> addParameter "@PTS" player.PTS
            |> executeScalar
            |> ignore
            |> Result.Ok
        with
        | e -> Result.Error (SqliteException e.Message)

    let getPlayerStats (connection: SqliteConnection) (PlayerID id) =
        let dataReaderToStats (dataReader:SqliteDataReader)  =
            ignore (dataReader.Read())
            let name = dataReader.GetString(0)
            let player = createPlayer name id
            let rec addStatToList list idx =
                if idx < 1 then list
                else addStatToList (dataReader.[idx].ToString()::list) (idx-1)
            let statList = addStatToList [] (dataReader.FieldCount-1)
            updateStats statList Database player
        try                           
            let command = connection.CreateCommand()
            command
            |> setText SqlCommands.GetPlayerStats
            |> addParameter "@ID" id
            |> executeReader
            |> dataReaderToStats
            |> Result.Ok
        with
        | e -> Result.Error (SqliteException e.Message)

    let GetInstance database = 
        { 
            new IPlayerDataAccess with
            member this.Connection = SqliteHelpers.createConnection database
            member this.PlayerExists playerID = playerExists this.Connection playerID
            member this.InsertPlayerStats player = insertPlayerStats this.Connection player
            member this.GetStatsForPlayer id = getPlayerStats this.Connection id
        }