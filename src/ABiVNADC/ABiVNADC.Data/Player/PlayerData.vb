Public Module PlayerData
    Friend Const TableName = "Players"
    Friend Const PlayerIdColumn = "PlayerId"
    Friend Const CharacterIdColumn = CharacterData.CharacterIdColumn
    Friend Sub Initialize()
        CharacterData.Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{PlayerIdColumn}] INT NOT NULL UNIQUE,
                [{CharacterIdColumn}] INT NOT NULL,
                FOREIGN KEY ([{CharacterIdColumn}]) REFERENCES [{CharacterData.TableName}]([{CharacterData.CharacterIdColumn}])
            );")
    End Sub
    Public Function ReadCharacter(playerId As Long) As Long?
        Return ReadColumnValue(Of Long, Long)(AddressOf Initialize, TableName, CharacterIdColumn, (PlayerIdColumn, playerId))
    End Function

    Public Sub Write(playerId As Long, characterId As Long)
        ReplaceRecord(AddressOf Initialize, TableName, (PlayerIdColumn, playerId), (CharacterIdColumn, characterId))
    End Sub

    Friend Sub ClearForCharacter(characterId As Long)
        ClearForColumnValue(AddressOf Initialize, TableName, (CharacterIdColumn, characterId))
    End Sub
End Module
