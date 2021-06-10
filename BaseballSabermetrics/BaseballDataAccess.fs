namespace Sabermetrics.Baseball
open Microsoft.Data.Sqlite
open Domain
open Stats
open Sabermetrics.Exceptions
open Sabermetrics.Dependencies.SqliteHelpers


[<RequireQualifiedAccess>]
module SqlCommands =
    [<Literal>] 
    let PlayerExists = """
    SELECT 1 FROM Hitters
    WHERE ID = @ID
    LIMIT 1
    """
    [<Literal>]
    let InsertHitterStats = """
    INSERT INTO Hitters (ID, Name, G, PA, AB, R, H, Doubles, Triples, HR, 
    RBI, SB, CS, BB, SO, TB, GDP, HBP, SH, SF, IBB)
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
    ,@TB
    ,@GDP
    ,@HBP
    ,@SH
    ,@SF
    ,@IBB
    );
    """

    [<Literal>]
    let GetHitterStats = """
    SELECT Name, G, PA, AB, R, H, Doubles, Triples, HR, 
    RBI, SB, CS, BB, SO, TB, GDP, HBP, SH, SF, IBB
    FROM Hitters
    WHERE ID = @ID
    """

module PlayerDataAccess =
    open Sabermetrics.Dependencies
    type IPlayerDataAccess =
        abstract member Connection: SqliteConnection
        abstract member PlayerExists: PlayerID -> Result<obj,Exception>
        abstract member InsertPlayerStats: Player -> Result<unit,Exception>
        abstract member GetStatsForHitter: PlayerID -> Result<Player,Exception>
        abstract member GetStatsForPitcher: PlayerID -> Result<Player,Exception>

    let private playerExists (connection:SqliteConnection) (PlayerID id) =
        try
            connection.CreateCommand()
            |> setText SqlCommands.PlayerExists
            |> addParameter "@ID" id
            |> executeScalar
            |> Result.Ok
        with
        | e -> Result.Error (SqliteException (sprintf "Unable to find player with id: %s" id))

    let private insertHitterStats (connection: SqliteConnection) (hitter: Hitter) =
        try
            let command = connection.CreateCommand()
            command
            |> setText SqlCommands.InsertHitterStats
            |> addParameter "@ID" hitter.ID
            |> addParameter "@Name" hitter.Name
            |> addParameter "@G" hitter.G
            |> addParameter "@PA" hitter.PA
            |> addParameter "@AB" hitter.AB
            |> addParameter "@R" hitter.R
            |> addParameter "@Doubles" hitter.Doubles
            |> addParameter "@Triples" hitter.Triples
            |> addParameter "@H" hitter.H
            |> addParameter "@HR" hitter.HR
            |> addParameter "@RBI" hitter.RBI
            |> addParameter "@SB" hitter.SB
            |> addParameter "@CS" hitter.CS
            |> addParameter "@BB" hitter.BB
            |> addParameter "@SO" hitter.SO
            |> addParameter "@TB" hitter.TB
            |> addParameter "@GDP" hitter.GDP
            |> addParameter "@HBP" hitter.HBP
            |> addParameter "@SH" hitter.SH
            |> addParameter "@SF" hitter.SF
            |> addParameter "@IBB" hitter.IBB
            |> executeScalar
            |> ignore
            |> Result.Ok
        with
        | e -> Result.Error (SqliteException e.Message)

    let insertPitcherStats (connection: SqliteConnection) (pitcher: Pitcher) =
        let command = connection.CreateCommand()
        Result.Ok ()


    let getHitterStats (connection: SqliteConnection) (PlayerID id) =
        let dataReaderToStats (dataReader:SqliteDataReader)  =
            ignore (dataReader.Read())
            let name = dataReader.GetString(0)
            let player = createHitter name id
            let rec addStatToList list idx =
                if idx < 1 then list
                else addStatToList (dataReader.[idx].ToString()::list) (idx-1)
            let statList = addStatToList [] (dataReader.FieldCount-1)
            updateStats statList Database player
        try                           
            let command = connection.CreateCommand()
            command
            |> setText SqlCommands.GetHitterStats
            |> addParameter "@ID" id
            |> executeReader
            |> dataReaderToStats
            |> Result.Ok
        with
        | e -> Result.Error (SqliteException e.Message)

    let getPitcherStats (connection: SqliteConnection) (PlayerID id) = 
        Result.Ok (createPitcher "Me" id)

    let GetInstance database = 
        { 
            new IPlayerDataAccess with
            member this.Connection = SqliteHelpers.createConnection database
            member this.PlayerExists playerID = playerExists this.Connection playerID
            member this.InsertPlayerStats player = 
                match player with
                | Hitter hitter -> insertHitterStats this.Connection hitter
                | Pitcher pitcher -> insertPitcherStats this.Connection pitcher
            member this.GetStatsForHitter id = getHitterStats this.Connection id
            member this.GetStatsForPitcher id = getPitcherStats this.Connection id
        }
