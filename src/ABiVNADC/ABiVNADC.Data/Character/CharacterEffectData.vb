Public Module CharacterEffectData
    Friend Const TableName = "ChracterEffects"
    Friend Const CharacterIdColumn = CharacterData.CharacterIdColumn
    Friend Const EffectTypeColumn = "EffectType"
    Friend Const DurationColumn = "Duration"
    Friend Sub Initialize()
        CharacterData.Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{CharacterIdColumn}],
                [{EffectTypeColumn}],
                [{DurationColumn}],
                UNIQUE([{CharacterIdColumn}],[{EffectTypeColumn}]),
                FOREIGN KEY([{CharacterIdColumn}]) REFERENCES [{CharacterData.TableName}]([{CharacterData.CharacterIdColumn}])
            );")
    End Sub
    Public Function ReadForCharacter(characterId As Long) As IEnumerable(Of Long)
        Return ReadRecordsWithColumnValue(Of Long, Long)(AddressOf Initialize, TableName, EffectTypeColumn, (CharacterIdColumn, characterId))
    End Function

    Public Function Read(characterId As Long, effectType As Long) As Long?
        Return ReadColumnValue(Of Long, Long, Long)(AddressOf Initialize, TableName, DurationColumn, (CharacterIdColumn, characterId), (EffectTypeColumn, effectType))
    End Function

    Public Sub Write(characterId As Long, effectType As Long, duration As Long)
        ReplaceRecord(AddressOf Initialize, TableName, (CharacterIdColumn, characterId), (EffectTypeColumn, effectType), (DurationColumn, duration))
    End Sub

    Public Sub Clear(characterId As Long, effectType As Long)
        ClearForColumnValues(AddressOf Initialize, TableName, (CharacterIdColumn, characterId), (EffectTypeColumn, effectType))
    End Sub

    Friend Sub ClearForCharacter(characterId As Long)
        ClearForColumnValue(AddressOf Initialize, TableName, (CharacterIdColumn, characterId))
    End Sub
End Module
