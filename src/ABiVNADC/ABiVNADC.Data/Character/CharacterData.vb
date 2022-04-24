Public Module CharacterData
    Friend Const TableName = "Characters"
    Friend Const CharacterIdColumn = "CharacterId"
    Friend Const CharacterNameColumn = "CharacterName"
    Friend Const CharacterTypeColumn = "CharacterType"
    Friend Const CharacterLevelColumn = "CharacterLevel"
    Friend Const WoundsColumn = "Wounds"
    Friend Const FatigueColumn = "Fatigue"

    Friend Sub Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{CharacterIdColumn}] INTEGER PRIMARY KEY AUTOINCREMENT,
                [{CharacterNameColumn}] TEXT NOT NULL,
                [{CharacterTypeColumn}] INT NOT NULL,
                [{CharacterLevelColumn}] INT NOT NULL,
                [{WoundsColumn}] INT NOT NULL DEFAULT (0),
                [{FatigueColumn}] INT NOT NULL DEFAULT (0)
            );")
    End Sub

    Public Function Create(characterName As String, characterType As Long, characterLevel As Long) As Long
        Initialize()
        ExecuteNonQuery(
            $"INSERT INTO [{TableName}]
            (
                [{CharacterNameColumn}],
                [{CharacterTypeColumn}],
                [{CharacterLevelColumn}]
            )
            VALUES
            (
                @{CharacterNameColumn},
                @{CharacterTypeColumn},
                @{CharacterLevelColumn}
            );",
            MakeParameter($"@{CharacterNameColumn}", characterName),
            MakeParameter($"@{CharacterTypeColumn}", characterType),
            MakeParameter($"@{CharacterLevelColumn}", characterLevel))
        Return LastInsertRowId
    End Function

    Public Sub WriteWounds(characterId As Long, wounds As Long)
        WriteColumnValue(AddressOf Initialize, TableName, CharacterIdColumn, characterId, WoundsColumn, wounds)
    End Sub

    Public Sub Clear(characterId As Long)
        CharacterInventoryData.ClearForCharacter(characterId)
        CharacterLocationData.Clear(characterId)
        PlayerCharacterData.ClearForCharacter(characterId)
        PlayerData.ClearForCharacter(characterId)
        ClearForColumnValue(AddressOf Initialize, TableName, CharacterIdColumn, characterId)
    End Sub

    Public Function ReadWounds(characterId As Long) As Long?
        Return ReadColumnValue(Of Long)(AddressOf Initialize, TableName, CharacterIdColumn, characterId, WoundsColumn)
    End Function

    Public Function ReadCharacterType(characterId As Long) As Long?
        Return ReadColumnValue(Of Long)(AddressOf Initialize, TableName, CharacterIdColumn, characterId, CharacterTypeColumn)
    End Function

    Public Function ReadLevel(characterId As Long) As Long?
        Return ReadColumnValue(Of Long)(AddressOf Initialize, TableName, CharacterIdColumn, characterId, CharacterLevelColumn)
    End Function

    Public Function ReadName(characterId As Long) As String
        Return ReadColumnString(AddressOf Initialize, TableName, CharacterIdColumn, characterId, CharacterNameColumn)
    End Function

    Public Function ReadFatigue(characterId As Long) As Long?
        Return ReadColumnValue(Of Long)(AddressOf Initialize, TableName, CharacterIdColumn, characterId, FatigueColumn)
    End Function

    Public Sub WriteFatigue(characterId As Long, fatigue As Long)
        WriteColumnValue(AddressOf Initialize, TableName, CharacterIdColumn, characterId, FatigueColumn, fatigue)
    End Sub
End Module
