namespace Sabermetrics.Dependencies
open Microsoft.Data.Sqlite
module SqliteHelpers =
    let setText text (command:SqliteCommand) =
        command.CommandText <- text
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