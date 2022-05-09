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
        Initialize()
        ExecuteNonQuery(
            $"INSERT INTO [{TableName}]
            (
                [{PlayerIdColumn}],
                [{TombstoneTextColumn}]
            ) 
            VALUES
            (
                @{PlayerIdColumn},
                @{TombstoneTextColumn}
            );",
            MakeParameter($"@{PlayerIdColumn}", playerId),
            MakeParameter($"@{TombstoneTextColumn}", tombstoneText))
    End Sub

    Public Function ReadTombstoneTexts(playerId As Long) As IEnumerable(Of String)
        Initialize()
        Return ExecuteReader(
            Function(reader) CStr(reader(TombstoneTextColumn)),
            $"SELECT [{TombstoneTextColumn}] FROM [{TableName}] WHERE [{PlayerIdColumn}]=@{PlayerIdColumn};",
            MakeParameter($"@{PlayerIdColumn}", playerId))
    End Function
End Module
