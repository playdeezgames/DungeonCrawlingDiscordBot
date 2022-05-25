Public Module CharacterPoisoningData
    Friend Const TableName = "CharacterPoisonings"
    Friend Const CharacterIdColumn = CharacterData.CharacterIdColumn
    Friend Const PoisoningColumn = "Poisoning"
    Friend Sub Initialize()
        CharacterData.Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{CharacterIdColumn}] INT NOT NULL,
                [{PoisoningColumn}] TEXT NOT NULL,
                FOREIGN KEY ([{CharacterIdColumn}]) REFERENCES [{CharacterData.TableName}]([{CharacterData.CharacterIdColumn}])
            );")
    End Sub
    Public Function Read(characterId As Long) As IEnumerable(Of String)
        Initialize()
        Return ExecuteReader(
            Function(reader) CStr(reader(PoisoningColumn)),
            $"SELECT [{PoisoningColumn}] FROM [{TableName}] WHERE [{CharacterIdColumn}]=@{CharacterIdColumn};",
            MakeParameter($"@{CharacterIdColumn}", characterId))
    End Function

    Public Sub Write(characterId As Long, poisonDice As String)
        CreateRecord(AddressOf Initialize, TableName, (CharacterIdColumn, characterId), (PoisoningColumn, poisonDice))
    End Sub

    Public Sub Clear(characterId As Long)
        ClearForColumnValue(AddressOf Initialize, TableName, CharacterIdColumn, characterId)
    End Sub
End Module
