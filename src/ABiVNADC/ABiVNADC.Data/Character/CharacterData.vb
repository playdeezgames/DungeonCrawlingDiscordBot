Public Module CharacterData
    Friend Const TableName = "Characters"
    Friend Const CharacterIdColumn = "CharacterId"
    Friend Const CharacterNameColumn = "CharacterName"
    Friend Const CharacterTypeColumn = "CharacterType"
    Friend Const CharacterLevelColumn = "CharacterLevel"
    Friend Const ExperienceColumn = "Experience"
    Friend Const LocationIdColumn = LocationData.LocationIdColumn

    Friend Sub Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{CharacterIdColumn}] INTEGER PRIMARY KEY AUTOINCREMENT,
                [{LocationIdColumn}] INT NOT NULL,
                [{CharacterNameColumn}] TEXT NOT NULL,
                [{CharacterTypeColumn}] INT NOT NULL,
                [{CharacterLevelColumn}] INT NOT NULL,
                [{ExperienceColumn}] INT NOT NULL DEFAULT (0),
                FOREIGN KEY ([{LocationIdColumn}]) REFERENCES [{LocationData.TableName}]([{LocationData.LocationIdColumn}])
            );")
    End Sub

    Public Sub WriteName(characterId As Long, newName As String)
        WriteColumnValue(AddressOf Initialize, TableName, CharacterIdColumn, characterId, CharacterNameColumn, newName)
    End Sub

    Public Function Exists(characterId As Long) As Boolean
        Return ReadLongsWithColumnValue(Of Long, Long)(AddressOf Initialize, TableName, CharacterIdColumn, (CharacterIdColumn, characterId)).Any
    End Function

    Public Function Create(characterName As String, characterType As Long, characterLevel As Long, locationId As Long) As Long
        Return CreateRecord(
            AddressOf Initialize,
            TableName,
            (CharacterNameColumn, characterName),
            (CharacterTypeColumn, characterType),
            (CharacterLevelColumn, characterLevel),
            (LocationIdColumn, locationId))
    End Function

    Public Sub Clear(characterId As Long)
        CharacterEquipSlotData.ClearForCharacter(characterId)
        CharacterInventoryData.ClearForCharacter(characterId)
        PlayerCharacterData.ClearForCharacter(characterId)
        PlayerData.ClearForCharacter(characterId)
        ShoppeAccountsData.ClearForCharacter(characterId)
        LocationOwnerData.ClearForCharacter(characterId)
        CharacterEffectData.ClearForCharacter(characterId)
        CharacterQuestData.Clear(characterId)
        CharacterEndowmentData.Clear(characterId)
        CharacterStatisticData.ClearForCharacter(characterId)
        FeatureOwnerData.ClearForCharacter(characterId)
        CharacterPoisoningData.Clear(characterId)
        ClearForColumnValue(AddressOf Initialize, TableName, CharacterIdColumn, characterId)
    End Sub

    Public Function ReadCharacterType(characterId As Long) As Long?
        Return ReadColumnValue(Of Long)(AddressOf Initialize, TableName, CharacterIdColumn, characterId, CharacterTypeColumn)
    End Function

    Public Function ReadLevel(characterId As Long) As Long?
        Return ReadColumnValue(Of Long)(AddressOf Initialize, TableName, CharacterIdColumn, characterId, CharacterLevelColumn)
    End Function

    Public Function ReadLocation(characterId As Long) As Long?
        Return ReadColumnValue(Of Long)(AddressOf Initialize, TableName, CharacterIdColumn, characterId, LocationIdColumn)
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

    Public Sub WriteLocation(characterId As Long, locationId As Long)
        WriteColumnValue(AddressOf Initialize, TableName, CharacterIdColumn, characterId, LocationIdColumn, locationId)
    End Sub

    Public Function ReadCharacterLevel(characterId As Long) As Long?
        Return ReadColumnValue(Of Long)(AddressOf Initialize, TableName, CharacterIdColumn, characterId, CharacterLevelColumn)
    End Function

    Public Function ReadForLocation(locationId As Long) As IEnumerable(Of Long)
        Return ReadLongsWithColumnValue(Of Long, Long)(AddressOf Initialize, TableName, CharacterIdColumn, (LocationIdColumn, locationId))
    End Function
End Module
