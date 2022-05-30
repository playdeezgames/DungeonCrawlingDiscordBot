Public Module CharacterEndowmentData
    Friend Const TableName = "CharacterEndowments"
    Friend Const CharacterIdColumn = CharacterData.CharacterIdColumn
    Friend Const SpentColumn = "Spent"

    Friend Sub Initialize()
        CharacterData.Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{CharacterIdColumn}] INT NOT NULL UNIQUE,
                [{SpentColumn}] INT NOT NULL,
                FOREIGN KEY ([{CharacterIdColumn}]) REFERENCES [{CharacterData.TableName}]([{CharacterData.CharacterIdColumn}])
            );")
    End Sub

    Function Read(characterId As Long) As Long?
        Return ReadColumnValue(Of Long, Long)(AddressOf Initialize, TableName, SpentColumn, (CharacterIdColumn, characterId))
    End Function

    Sub Clear(characterId As Long)
        ClearForColumnValue(AddressOf Initialize, TableName, CharacterIdColumn, characterId)
    End Sub
End Module
