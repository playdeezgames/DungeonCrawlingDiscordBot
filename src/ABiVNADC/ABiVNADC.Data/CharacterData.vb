Public Module CharacterData
    Friend Const TableName = "Characters"
    Friend Const CharacterIdColumn = "CharacterId"
    Friend Const CharacterNameColumn = "CharacterName"

    Friend Sub Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{CharacterIdColumn}] INTEGER PRIMARY KEY AUTOINCREMENT,
                [{CharacterNameColumn}] TEXT NOT NULL
            );")
    End Sub

    Public Function Create(characterName As String) As Long
        Initialize()
        ExecuteNonQuery(
            $"INSERT INTO [{TableName}]
            (
                [{CharacterNameColumn}]
            )
            VALUES
            (
                @{CharacterNameColumn}
            );",
            MakeParameter($"@{CharacterNameColumn}", characterName))
        Return LastInsertRowId
    End Function

    Public Function ReadName(characterId As Long) As String
        Return ReadColumnString(AddressOf Initialize, TableName, CharacterIdColumn, characterId, CharacterNameColumn)
    End Function
End Module
