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
