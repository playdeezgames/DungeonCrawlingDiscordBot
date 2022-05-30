Public Module PlayerIncentiveLevelData
    Friend Const TableName = "PlayerIncentiveLevels"
    Friend Const PlayerIdColumn = PlayerData.PlayerIdColumn
    Friend Const IncentiveTypeColumn = "IncentiveType"
    Friend Const LevelColumn = "Level"
    Friend Sub Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{PlayerIdColumn}] INT NOT NULL,
                [{IncentiveTypeColumn}] INT NOT NULL,
                [{LevelColumn}] INT NOT NULL,
                UNIQUE([{PlayerIdColumn}],[{IncentiveTypeColumn}])
            );")
    End Sub
    Public Function ReadForPlayer(playerId As Long) As Dictionary(Of Long, Long)
        Return ReadRecordsWithColumnValue(Of Long, Long, Long)(
            AddressOf Initialize,
            TableName,
            (IncentiveTypeColumn, LevelColumn),
            (PlayerIdColumn, playerId)).ToDictionary(Function(x) x.Item1, Function(x) x.Item2)
    End Function

    Public Function Read(playerId As Long, incentiveType As Long) As Long?
        Return ReadColumnValue(Of Long, Long, Long)(AddressOf Initialize, TableName, LevelColumn, (PlayerIdColumn, playerId), (IncentiveTypeColumn, incentiveType))
    End Function

    Public Sub Write(playerId As Long, incentiveType As Long, level As Long)
        ReplaceRecord(
            AddressOf Initialize,
            TableName,
            (PlayerIdColumn, playerId),
            (IncentiveTypeColumn, incentiveType),
            (LevelColumn, level))
    End Sub
End Module
