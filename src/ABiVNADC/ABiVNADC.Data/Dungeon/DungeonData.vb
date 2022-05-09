﻿Public Module DungeonData
    Friend Const TableName = "Dungeons"
    Friend Const DungeonIdColumn = "DungeonId"
    Friend Const DungeonNameColumn = "DungeonName"
    Friend Const StartingLocationIdColumn = "StartingLocationId"
    Friend Const PlayerIdColumn = PlayerData.PlayerIdColumn
    Friend Const DifficultyColumn = "Difficulty"
    Friend Sub Initialize()
        LocationData.Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{DungeonIdColumn}] INTEGER PRIMARY KEY AUTOINCREMENT,
                [{DungeonNameColumn}] TEXT NOT NULL,
                [{PlayerIdColumn}] INT NULL,
                [{StartingLocationIdColumn}] INT NOT NULL,
                [{DifficultyColumn}] INT NOT NULL
            );")
    End Sub

    Public Sub Clear(dungeonId As Long)
        DungeonLocationData.ClearForDungeon(dungeonId)
        ClearForColumnValue(AddressOf Initialize, TableName, DungeonIdColumn, dungeonId)
    End Sub

    Public Function ReadLocation(dungeonId As Long) As Long?
        Return ReadColumnValue(Of Long)(AddressOf Initialize, TableName, DungeonIdColumn, dungeonId, StartingLocationIdColumn)
    End Function


    Public Function Create(dungeonName As String, startingLocationId As Long, difficulty As Long) As Long
        Initialize()
        ExecuteNonQuery(
            $"INSERT INTO [{TableName}]
            (
                [{DungeonNameColumn}],
                [{StartingLocationIdColumn}],
                [{DifficultyColumn}]
            ) 
            VALUES
            (
                @{DungeonNameColumn},
                @{StartingLocationIdColumn},
                @{DifficultyColumn}
            );",
            MakeParameter($"@{DungeonNameColumn}", dungeonName),
            MakeParameter($"@{DifficultyColumn}", difficulty),
            MakeParameter($"@{StartingLocationIdColumn}", startingLocationId))
        Return LastInsertRowId
    End Function

    Public Function Create(playerId As Long, dungeonName As String, startingLocationId As Long, difficulty As Long) As Long
        Initialize()
        ExecuteNonQuery(
            $"INSERT INTO [{TableName}]
            (
                [{PlayerIdColumn}],
                [{DungeonNameColumn}],
                [{StartingLocationIdColumn}],
                [{DifficultyColumn}]
            ) 
            VALUES
            (
                @{PlayerIdColumn},
                @{DungeonNameColumn},
                @{StartingLocationIdColumn},
                @{DifficultyColumn}
            );",
            MakeParameter($"@{PlayerIdColumn}", playerId),
            MakeParameter($"@{DungeonNameColumn}", dungeonName),
            MakeParameter($"@{DifficultyColumn}", difficulty),
            MakeParameter($"@{StartingLocationIdColumn}", startingLocationId))
        Return LastInsertRowId
    End Function

    Public Function ReadCountForPlayerAndDungeonName(playerId As Long, dungeonName As String) As Long
        Initialize()
        Return ExecuteScalar(Of Long)(
            $"SELECT COUNT(1) FROM [{TableName}] WHERE [{PlayerIdColumn}]=@{PlayerIdColumn} AND [{DungeonNameColumn}]=@{DungeonNameColumn};",
            MakeParameter($"@{PlayerIdColumn}", playerId),
            MakeParameter($"@{DungeonNameColumn}", dungeonName)).Value
    End Function

    Public Function ReadName(dungeonId As Long) As String
        Return ReadColumnString(AddressOf Initialize, TableName, DungeonIdColumn, dungeonId, DungeonNameColumn)
    End Function

    Public Function ReadDifficulty(dungeonId As Long) As Long?
        Return ReadColumnValue(Of Long)(AddressOf Initialize, TableName, DungeonIdColumn, dungeonId, DifficultyColumn)
    End Function

    Public Function ReadForPlayer(playerId As Long) As IEnumerable(Of Long)
        Return ReadIdsWithColumnValue(AddressOf Initialize, TableName, DungeonIdColumn, PlayerIdColumn, playerId)
    End Function
End Module
