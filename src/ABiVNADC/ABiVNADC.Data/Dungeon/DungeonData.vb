Public Module DungeonData
    Friend Const TableName = "Dungeons"
    Friend Const DungeonIdColumn = "DungeonId"
    Friend Const DungeonNameColumn = "DungeonName"
    Friend Const OverworldLocationIdColumn = "OverworldLocationId"
    Friend Const StartingLocationIdColumn = "StartingLocationId"
    Friend Const DifficultyColumn = "Difficulty"
    Friend Sub Initialize()
        LocationData.Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{DungeonIdColumn}] INTEGER PRIMARY KEY AUTOINCREMENT,
                [{DungeonNameColumn}] TEXT NOT NULL,
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
        Return CreateRecord(AddressOf Initialize, TableName, (DungeonNameColumn, dungeonName), (OverworldLocationIdColumn, overworldLocationId), (StartingLocationIdColumn, startingLocationId), (DifficultyColumn, difficulty))
    End Function

    Public Function ReadName(dungeonId As Long) As String
        Return ReadColumnString(AddressOf Initialize, TableName, DungeonIdColumn, dungeonId, DungeonNameColumn)
    End Function

    Public Function ReadDifficulty(dungeonId As Long) As Long?
        Return ReadColumnValue(Of Long)(AddressOf Initialize, TableName, DungeonIdColumn, dungeonId, DifficultyColumn)
    End Function
End Module
