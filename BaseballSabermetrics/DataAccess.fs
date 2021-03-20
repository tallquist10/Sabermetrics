namespace Sabermetrics
open Microsoft.Data.Sqlite
open Domain
module SqliteHelpers =
    let setText text (command:SqliteCommand) =
        command.CommandText = text |> ignore
        command

    let addParameter param value (command:SqliteCommand) =
        command.Parameters.AddWithValue(param, value) |> ignore
        command

    let executeScalar (command:SqliteCommand) =
        command.ExecuteScalar()

    let executeNonQuery (command:SqliteCommand) =
        command.ExecuteNonQuery()

    let executeReader (command:SqliteCommand) =
        command.ExecuteReader()

    let executeScalarAsync (command:SqliteCommand) =
        command.ExecuteScalarAsync()

    let executeNonQueryAsync (command:SqliteCommand) =
        command.ExecuteNonQueryAsync()

    let executeReaderAsync (command:SqliteCommand) =
        command.ExecuteReaderAsync()

[<RequireQualifiedAccess>]
module SqlCommands =
    [<Literal>] 
    let PlayerExists = """
    SELECT TOP 1 1 FROM baseball.Hitters WHERE ID = @ID
    """
    [<Literal>]
    let InsertHitterStats = """
    INSERT INTO baseball.Hitters (ID, Name, G, PA, AB, R, H, Doubles, Triples, HR, 
    RBI, SB, CS, BB, SO, BA, OBP, SLG, OPS, OPSPlus, TB, GDP, HBP, SH, SF, IBB)
    VALUES (
    @ID
    ,@Name
    ,@G
    ,@PA
    ,@AB
    ,@R
    ,@H
    ,@Doubles
    ,@Triples
    ,@HR
    ,@RBI
    ,@SB
    ,@CS
    ,@BB
    ,@SO
    ,@BA
    ,@OBP
    ,@SLG
    ,@OPS
    ,@OPSPlus
    ,@TB
    ,@GDP
    ,@HBP
    ,@SH
    ,@SF
    ,@IBB
    )
    """

module PlayerDataAccess =
    open SqliteHelpers
    type IPlayerDataAccess =
        abstract member connection: SqliteConnection
        abstract member PlayerExists: PlayerID -> Result<obj,Exception>
        abstract member InsertPlayerStats: Player -> Result<int,Exception>

    let private playerExists (connection:SqliteConnection) (PlayerID id) =
        try
            connection.CreateCommand()
            |> setText SqlCommands.PlayerExists
            |> addParameter "@ID" id
            |> executeScalar
            |> Result.Ok
        with
        | e -> Result.Error (SqliteException (sprintf "Unable to find player with id: %s" id))

    let private insertPlayerStats (connection: SqliteConnection) (player:Player) = 
        try
            let command = connection.CreateCommand()
            match player with
            | Hitter hitter -> 
                command
                |> setText SqlCommands.InsertHitterStats
                |> addParameter "@ID" hitter.Page
                |> addParameter "@Name" hitter.Name
                |> addParameter "@G" hitter.G
                |> addParameter "@PA" hitter.PA
                |> addParameter "@AB" hitter.AB
                |> addParameter "@R" hitter.R
                |> addParameter "@Doubles" hitter.Doubles
                |> addParameter "@Tripes" hitter.Triples
                |> addParameter "@H" hitter.H
                |> addParameter "@HR" hitter.HR
                |> addParameter "@RBI" hitter.RBI
                |> addParameter "@SB" hitter.SB
                |> addParameter "@CS" hitter.CS
                |> addParameter "@BB" hitter.BB
                |> addParameter "@SO" hitter.SO
                |> addParameter "@BA" hitter.BA
                |> addParameter "@OBP" hitter.OBP
                |> addParameter "@SLG" hitter.SLG
                |> addParameter "@OPS" hitter.OPS
                |> addParameter "@OPSPlus" hitter.OPSPlus
                |> addParameter "@TB" hitter.TB
                |> addParameter "@GDP" hitter.GDP
                |> addParameter "@HBP" hitter.HBP
                |> addParameter "@SH" hitter.SH
                |> addParameter "@SF" hitter.SF
                |> addParameter "@IBB" hitter.IBB
                |> executeNonQuery
                |> Result.Ok
            | Pitcher _ -> Result.Ok 1 
        with
        | e -> Result.Error (SqliteException (sprintf "Unable to insert player into baseball.Hitters. Exception: %s" e.Message))

    let GetInstance connectionString = {
        new IPlayerDataAccess with
        member this.connection = new SqliteConnection(connectionString)
        member this.PlayerExists playerID = playerExists this.connection playerID
        member this.InsertPlayerStats player = insertPlayerStats this.connection player
    }
