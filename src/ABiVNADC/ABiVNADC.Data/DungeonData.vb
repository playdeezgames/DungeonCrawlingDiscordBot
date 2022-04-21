Public Module DungeonData
    Friend Const TableName = "Dungeons"
    Friend Const DungeonIdColumn = "DungeonId"
    Friend Const DungeonNameColumn = "DungeonName"
    Friend Const StartingLocationIdColumn = "StartingLocationId"
    Friend Const PlayerIdColumn = PlayerData.PlayerIdColumn
    Friend Sub Initialize()
        LocationData.Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{DungeonIdColumn}] INTEGER PRIMARY KEY AUTOINCREMENT,
                [{DungeonNameColumn}] TEXT NOT NULL,
                [{PlayerIdColumn}] INT NOT NULL,
                [{StartingLocationIdColumn}] INT NOT NULL,
                UNIQUE([{PlayerIdColumn}],[{DungeonNameColumn}])
            );")
    End Sub

    Public Function Create(playerId As Long, dungeonName As String, startingLocationId As Long) As Long
        Initialize()
        ExecuteNonQuery(
            $"INSERT INTO [{TableName}]
            (
                [{PlayerIdColumn}],
                [{DungeonNameColumn}],
                [{StartingLocationIdColumn}]
            ) 
            VALUES
            (
                @{PlayerIdColumn},
                @{DungeonNameColumn},
                @{StartingLocationIdColumn}
            );",
            MakeParameter($"@{PlayerIdColumn}", playerId),
            MakeParameter($"@{DungeonNameColumn}", dungeonName),
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

    Public Function ReadForPlayer(playerId As Long) As IEnumerable(Of Long)
        Return ReadIdsWithColumnValue(AddressOf Initialize, TableName, DungeonIdColumn, PlayerIdColumn, playerId)
    End Function
End Module
