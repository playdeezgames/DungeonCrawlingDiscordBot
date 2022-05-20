Public Module LocationOwnerData
    Friend Const TableName = "LocationOwners"
    Friend Const LocationIdColumn = LocationData.LocationIdColumn
    Friend Const CharacterIdColumn = CharacterData.CharacterIdColumn
    Friend Sub Initialize()
        LocationData.Initialize()
        CharacterData.Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS[{TableName}]
            (
                [{LocationIdColumn}] INT NOT NULL UNIQUE,
                [{CharacterIdColumn}] INT NOT NULL,
                FOREIGN KEY([{LocationIdColumn}]) REFERENCES [{LocationData.TableName}]([{LocationData.LocationIdColumn}]),
                FOREIGN KEY([{CharacterIdColumn}]) REFERENCES [{CharacterData.TableName}]([{CharacterData.CharacterIdColumn}])
            );")
    End Sub
    Public Function Read(locationId As Long) As Long?
        Return ReadColumnValue(Of Long)(AddressOf Initialize, TableName, LocationIdColumn, locationId, CharacterIdColumn)
    End Function

    Public Sub Write(locationId As Long, characterId As Long)
        ReplaceRecord(AddressOf Initialize, TableName, LocationIdColumn, locationId, CharacterIdColumn, characterId)
    End Sub

    Friend Sub ClearForCharacter(characterId As Object)
        ClearForColumnValue(AddressOf Initialize, TableName, CharacterIdColumn, characterId)
    End Sub
End Module
