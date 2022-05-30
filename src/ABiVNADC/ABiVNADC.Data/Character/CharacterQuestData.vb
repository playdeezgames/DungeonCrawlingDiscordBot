Public Module CharacterQuestData
    Friend Const TableName = "CharacterQuests"
    Friend Const CharacterIdColumn = CharacterData.CharacterIdColumn
    Friend Const FeatureIdColumn = FeatureData.FeatureIdColumn
    Friend Sub Initialize()
        CharacterData.Initialize()
        FeatureData.Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{CharacterIdColumn}] INT NOT NULL UNIQUE,
                [{FeatureIdColumn}] INT NOT NULL,
                FOREIGN KEY ([{CharacterIdColumn}]) REFERENCES [{CharacterData.TableName}]([{CharacterData.CharacterIdColumn}]),
                FOREIGN KEY ([{FeatureIdColumn}]) REFERENCES [{FeatureData.TableName}]([{FeatureData.FeatureIdColumn}])
            );")
    End Sub

    Public Sub Write(characterId As Long, featureId As Long)
        ReplaceRecord(AddressOf Initialize, TableName, CharacterIdColumn, characterId, FeatureIdColumn, featureId)
    End Sub

    Public Function ReadFeature(characterId As Long) As Long?
        Return ReadColumnValue(Of Long, Long)(AddressOf Initialize, TableName, FeatureIdColumn, (CharacterIdColumn, characterId))
    End Function

    Public Sub Clear(characterId As Long)
        ClearForColumnValue(AddressOf Initialize, TableName, CharacterIdColumn, characterId)
    End Sub
End Module
