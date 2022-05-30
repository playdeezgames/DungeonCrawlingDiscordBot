Public Module PlayerIncentiveData
    Friend Const TableName = "PlayerIncentives"
    Friend Const PlayerIdColumn = PlayerData.PlayerIdColumn
    Friend Const IncentivePointsColumn = "IncentivePoints"
    Friend Sub Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{PlayerIdColumn}] INT NOT NULL UNIQUE,
                [{IncentivePointsColumn}] INT NOT NULL
            );")
    End Sub
    Public Function Read(playerId As Long) As Long?
        Return ReadColumnValue(Of Long, Long)(AddressOf Initialize, TableName, IncentivePointsColumn, (PlayerIdColumn, playerId))
    End Function

    Public Sub Write(playerId As Long, incentivePoints As Long)
        ReplaceRecord(AddressOf Initialize, TableName, (PlayerIdColumn, playerId), (IncentivePointsColumn, incentivePoints))
    End Sub
End Module
