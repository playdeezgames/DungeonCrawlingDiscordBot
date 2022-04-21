Public Module CharacterLocationData
    Friend Const TableName = "CharacterLocations"
    Friend Const CharacterIdColumn = CharacterData.CharacterIdColumn
    Friend Const LocationIdColumn = LocationData.LocationIdColumn
    Friend Sub Initialize()
        CharacterData.Initialize()
        LocationData.Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{CharacterIdColumn}] INT NOT NULL UNIQUE,
                [{LocationIdColumn}] INT NOT NULL,
                FOREIGN KEY ([{CharacterIdColumn}]) REFERENCES [{CharacterData.TableName}]([{CharacterData.CharacterIdColumn}]),
                FOREIGN KEY ([{LocationIdColumn}]) REFERENCES [{LocationData.TableName}]([{LocationData.LocationIdColumn}])
            );")
    End Sub
    Public Function Read(characterId As Long) As Long?
        Return ReadColumnValue(Of Long)(AddressOf Initialize, TableName, CharacterIdColumn, characterId, LocationIdColumn)
    End Function

    Public Sub Clear(characterId As Long)
        ClearForColumnValue(AddressOf Initialize, TableName, CharacterIdColumn, characterId)
    End Sub

    Public Sub Write(characterId As Long, locationId As Long)
        Initialize()
        ExecuteNonQuery(
            $"REPLACE INTO [{TableName}]([{CharacterIdColumn}],[{LocationIdColumn}]) VALUES(@{CharacterIdColumn},@{LocationIdColumn});",
            MakeParameter($"@{CharacterIdColumn}", characterId),
            MakeParameter($"@{LocationIdColumn}", locationId))
    End Sub
End Module
