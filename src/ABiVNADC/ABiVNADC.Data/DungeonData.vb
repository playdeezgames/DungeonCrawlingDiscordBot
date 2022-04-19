Public Module DungeonData
    Friend Const TableName = "Dungeons"
    Friend Const DungeonIdColumn = "DungeonId"
    Friend Const DungeonNameColumn = "DungeonName"
    Friend Const PlayerIdColumn = PlayerData.PlayerIdColumn
    Friend Sub Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{DungeonIdColumn}] INTEGER PRIMARY KEY AUTOINCREMENT,
                [{DungeonNameColumn}] TEXT NOT NULL,
                [{PlayerIdColumn}] INT NOT NULL
            );")
    End Sub

    Public Function ReadName(dungeonId As Long) As String
        Return ReadColumnString(AddressOf Initialize, TableName, DungeonIdColumn, dungeonId, DungeonNameColumn)
    End Function

    Public Function ReadForPlayer(playerId As Long) As IEnumerable(Of Long)
        Return ReadIdsWithColumnValue(AddressOf Initialize, TableName, DungeonIdColumn, PlayerIdColumn, playerId)
    End Function
End Module
