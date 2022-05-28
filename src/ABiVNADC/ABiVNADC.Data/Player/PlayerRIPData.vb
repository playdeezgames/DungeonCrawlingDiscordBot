Public Module PlayerRIPData
    Friend Const TableName = "PlayerRips"
    Friend Const RipIdColumn = "RipId"
    Friend Const PlayerIdColumn = PlayerData.PlayerIdColumn
    Friend Const TombstoneTextColumn = "TombstoneText"

    Friend Sub Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{RipIdColumn}] INTEGER PRIMARY KEY AUTOINCREMENT,
                [{PlayerIdColumn}] INT NOT NULL,
                [{TombstoneTextColumn}] TEXT NOT NULL
            );")
    End Sub

    Public Sub Write(playerId As Long, tombstoneText As String)
        CreateRecord(AddressOf Initialize, TableName, (PlayerIdColumn, playerId), (TombstoneTextColumn, tombstoneText))
    End Sub

    Public Function ReadTombstoneTexts(playerId As Long) As IEnumerable(Of String)
        Return ReadRecordsWithColumnValue(Of Long, String)(AddressOf Initialize, TableName, TombstoneTextColumn, (PlayerIdColumn, playerId))
    End Function
End Module
