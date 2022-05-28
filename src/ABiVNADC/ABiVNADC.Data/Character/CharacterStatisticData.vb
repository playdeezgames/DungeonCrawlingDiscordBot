Public Module CharacterStatisticData
    Friend Const TableName = "CharacterStatistics"
    Friend Const CharacterIdColumn = CharacterData.CharacterIdColumn
    Friend Const StatisticTypeColumn = "StatisticType"
    Friend Const AmountColumn = "Amount"
    Friend Sub Initialize()
        CharacterData.Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{CharacterIdColumn}] INT NOT NULL,
                [{StatisticTypeColumn}] INT NOT NULL,
                [{AmountColumn}] INT NOT NULL,
                UNIQUE([{CharacterIdColumn}],[{StatisticTypeColumn}]),
                FOREIGN KEY ([{CharacterIdColumn}]) REFERENCES [{CharacterData.TableName}]([{CharacterData.CharacterIdColumn}])
            );")
    End Sub
    Public Function Read(characterId As Long, statisticType As Long) As Long?
        Return ReadColumnValue(Of Long, Long, Long)(AddressOf Initialize, TableName, AmountColumn, (CharacterIdColumn, characterId), (StatisticTypeColumn, statisticType))
    End Function

    Friend Sub ClearForCharacter(characterId As Long)
        ClearForColumnValue(AddressOf Initialize, TableName, CharacterIdColumn, characterId)
    End Sub

    Public Sub Write(characterId As Long, statisticType As Long, amount As Long)
        ReplaceRecord(
            AddressOf Initialize,
            TableName,
            CharacterIdColumn, characterId,
            StatisticTypeColumn, statisticType,
            AmountColumn, amount)
    End Sub
End Module
