Public Module CharacterData
    Friend Const TableName = "Characters"
    Friend Const CharacterIdColumn = "CharacterId"
    Friend Const CharacterNameColumn = "CharacterName"
    Friend Const CharacterTypeColumn = "CharacterType"
    Friend Const CharacterLevelColumn = "CharacterLevel"
    Friend Const ExperienceColumn = "Experience"

    Friend Sub Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{CharacterIdColumn}] INTEGER PRIMARY KEY AUTOINCREMENT,
                [{CharacterNameColumn}] TEXT NOT NULL,
                [{CharacterTypeColumn}] INT NOT NULL,
                [{CharacterLevelColumn}] INT NOT NULL,
                [{ExperienceColumn}] INT NOT NULL DEFAULT (0)
            );")
    End Sub

    Public Sub WriteName(characterId As Long, newName As String)
        WriteColumnValue(AddressOf Initialize, TableName, CharacterIdColumn, characterId, CharacterNameColumn, newName)
    End Sub

    Public Function Exists(characterId As Long) As Boolean
        Return ReadIdsWithColumnValue(AddressOf Initialize, TableName, CharacterIdColumn, CharacterIdColumn, characterId).Any
    End Function

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

    Public Sub Clear(characterId As Long)
        CharacterEquipSlotData.ClearForCharacter(characterId)
        CharacterInventoryData.ClearForCharacter(characterId)
        CharacterLocationData.Clear(characterId)
        PlayerCharacterData.ClearForCharacter(characterId)
        PlayerData.ClearForCharacter(characterId)
        ShoppeAccountsData.ClearForCharacter(characterId)
        LocationOwnerData.ClearForCharacter(characterId)
        CharacterEffectData.ClearForCharacter(characterId)
        CharacterQuestData.Clear(characterId)
        CharacterEndowmentData.Clear(characterId)
        CharacterStatisticData.ClearForCharacter(characterId)
        ClearForColumnValue(AddressOf Initialize, TableName, CharacterIdColumn, characterId)
    End Sub

    Public Function ReadCharacterType(characterId As Long) As Long?
        Return ReadColumnValue(Of Long)(AddressOf Initialize, TableName, CharacterIdColumn, characterId, CharacterTypeColumn)
    End Function

    Public Function ReadLevel(characterId As Long) As Long?
        Return ReadColumnValue(Of Long)(AddressOf Initialize, TableName, CharacterIdColumn, characterId, CharacterLevelColumn)
    End Function

    Public Function ReadName(characterId As Long) As String
        Return ReadColumnString(AddressOf Initialize, TableName, CharacterIdColumn, characterId, CharacterNameColumn)
    End Function

    Public Function ReadExperience(characterId As Long) As Long?
        Return ReadColumnValue(Of Long)(AddressOf Initialize, TableName, CharacterIdColumn, characterId, ExperienceColumn)
    End Function

    Public Sub WriteExperience(characterId As Long, experience As Long)
        WriteColumnValue(AddressOf Initialize, TableName, CharacterIdColumn, characterId, ExperienceColumn, experience)
    End Sub

    Public Sub WriteCharacterLevel(characterId As Long, level As Long)
        WriteColumnValue(AddressOf Initialize, TableName, CharacterIdColumn, characterId, CharacterLevelColumn, level)
    End Sub

    Public Function ReadCharacterLevel(characterId As Long) As Long?
        Return ReadColumnValue(Of Long)(AddressOf Initialize, TableName, CharacterIdColumn, characterId, CharacterLevelColumn)
    End Function
End Module
