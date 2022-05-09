Public Module DungeonData
    Friend Const TableName = "Dungeons"
    Friend Const DungeonIdColumn = "DungeonId"
    Friend Const DungeonNameColumn = "DungeonName"
    Friend Const OverworldLocationIdColumn = "OverworldLocationId"
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
                [{OverworldLocationIdColumn}] INT NOT NULL,
                [{StartingLocationIdColumn}] INT NOT NULL,
                [{DifficultyColumn}] INT NOT NULL,
                FOREIGN KEY ([{StartingLocationIdColumn}]) REFERENCES [{LocationData.TableName}]([{LocationData.LocationIdColumn}]),
                FOREIGN KEY ([{OverworldLocationIdColumn}]) REFERENCES [{LocationData.TableName}]([{LocationData.LocationIdColumn}])
            );")
    End Sub

    Public Sub Clear(dungeonId As Long)
        DungeonLocationData.ClearForDungeon(dungeonId)
        ClearForColumnValue(AddressOf Initialize, TableName, DungeonIdColumn, dungeonId)
    End Sub

    Public Function ReadStartingLocation(dungeonId As Long) As Long?
        Return ReadColumnValue(Of Long)(AddressOf Initialize, TableName, DungeonIdColumn, dungeonId, StartingLocationIdColumn)
    End Function

    Public Function ReadOverworldLocation(dungeonId As Long) As Long?
        Return ReadColumnValue(Of Long)(AddressOf Initialize, TableName, DungeonIdColumn, dungeonId, OverworldLocationIdColumn)
    End Function

    Public Function Create(dungeonName As String, overworldLocationId As Long, startingLocationId As Long, difficulty As Long) As Long
        Initialize()
        ExecuteNonQuery(
            $"INSERT INTO [{TableName}]
            (
                [{DungeonNameColumn}],
                [{OverworldLocationIdColumn}],
                [{StartingLocationIdColumn}],
                [{DifficultyColumn}]
            ) 
            VALUES
            (
                @{DungeonNameColumn},
                @{OverworldLocationIdColumn},
                @{StartingLocationIdColumn},
                @{DifficultyColumn}
            );",
            MakeParameter($"@{DungeonNameColumn}", dungeonName),
            MakeParameter($"@{DifficultyColumn}", difficulty),
            MakeParameter($"@{StartingLocationIdColumn}", startingLocationId),
            MakeParameter($"@{OverworldLocationIdColumn}", overworldLocationId))
        Return LastInsertRowId
    End Function

    Public Function Create(playerId As Long, dungeonName As String, overworldLocationId As Long, startingLocationId As Long, difficulty As Long) As Long
        Initialize()
        ExecuteNonQuery(
            $"INSERT INTO [{TableName}]
            (
                [{PlayerIdColumn}],
                [{DungeonNameColumn}],
                [{OverworldLocationIdColumn}],
                [{StartingLocationIdColumn}],
                [{DifficultyColumn}]
            ) 
            VALUES
            (
                @{PlayerIdColumn},
                @{DungeonNameColumn},
                @{OverworldLocationIdColumn},
                @{StartingLocationIdColumn},
                @{DifficultyColumn}
            );",
            MakeParameter($"@{PlayerIdColumn}", playerId),
            MakeParameter($"@{DungeonNameColumn}", dungeonName),
            MakeParameter($"@{DifficultyColumn}", difficulty),
            MakeParameter($"@{StartingLocationIdColumn}", startingLocationId),
            MakeParameter($"@{OverworldLocationIdColumn}", overworldLocationId))
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
