Public Module FeatureOwnerData
    Friend Const TableName = "FeatureOwners"
    Friend Const FeatureIdColumn = FeatureData.FeatureIdColumn
    Friend Const CharacterIdColumn = CharacterData.CharacterIdColumn
    Friend Sub Initialize()
        FeatureData.Initialize()
        CharacterData.Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{FeatureIdColumn}] INT NOT NULL UNIQUE,
                [{CharacterIdColumn}] INT NOT NULL,
                FOREIGN KEY ([{FeatureIdColumn}]) REFERENCES [{FeatureData.TableName}]([{FeatureData.FeatureIdColumn}]),
                FOREIGN KEY ([{CharacterIdColumn}]) REFERENCES [{CharacterData.TableName}]([{CharacterData.CharacterIdColumn}])
            );")
    End Sub

    Public Function ReadForCharacter(characterId As Long) As IEnumerable(Of Long)
        Return ReadRecordsWithColumnValue(Of Long, Long)(AddressOf Initialize, TableName, FeatureIdColumn, (CharacterIdColumn, characterId))
    End Function

    Friend Sub ClearForCharacter(characterId As Long)
        ClearForColumnValue(AddressOf Initialize, TableName, CharacterIdColumn, characterId)
    End Sub

    Public Function ReadCharacter(featureId As Long) As Long?
        Return ReadColumnValue(Of Long)(AddressOf Initialize, TableName, FeatureIdColumn, featureId, CharacterIdColumn)
    End Function

    Public Sub Clear(featureId As Long)
        ClearForColumnValue(AddressOf Initialize, TableName, FeatureIdColumn, featureId)
    End Sub

    Public Sub Write(featureId As Long, characterId As Long)
        ReplaceRecord(AddressOf Initialize, TableName, FeatureIdColumn, featureId, CharacterIdColumn, characterId)
    End Sub
End Module
