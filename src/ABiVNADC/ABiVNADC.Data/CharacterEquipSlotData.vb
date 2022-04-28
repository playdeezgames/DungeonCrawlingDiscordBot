Public Module CharacterEquipSlotData
    Friend Const TableName = "CharacterEquipSlots"
    Friend Const CharacterIdColumn = CharacterData.CharacterIdColumn
    Friend Const ItemIdColumn = ItemData.ItemIdColumn
    Friend Const EquipSlotColumn = "EquipSlot"
    Friend Sub Initialize()
        CharacterData.Initialize()
        ItemData.Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{CharacterIdColumn}] INT NOT NULL,
                [{EquipSlotColumn}] INT NOT NULL,
                [{ItemIdColumn}] INT NOT NULL UNIQUE,
                UNIQUE([{CharacterIdColumn}],[{EquipSlotColumn}]),
                FOREIGN KEY ([{CharacterIdColumn}]) REFERENCES [{CharacterData.TableName}]([{CharacterData.CharacterIdColumn}]),
                FOREIGN KEY ([{ItemIdColumn}]) REFERENCES [{ItemData.TableName}]([{ItemData.ItemIdColumn}])
            );")
    End Sub

    Public Function ReadForCharacter(characterId As Long) As Dictionary(Of Long, Long)
        Initialize()
        Dim result As New Dictionary(Of Long, Long)
        For Each entry In ExecuteReader(
            Function(reader) New Tuple(Of Long, Long)(CLng(reader(EquipSlotColumn)), CLng(reader(ItemIdColumn))),
            $"SELECT [{EquipSlotColumn}], [{ItemIdColumn}] FROM [{TableName}] WHERE [{CharacterIdColumn}]=@{CharacterIdColumn};",
            MakeParameter($"@{CharacterIdColumn}", characterId))
            result(entry.Item1) = entry.Item2
        Next
        Return result
    End Function

    Public Sub ClearForItem(itemId As Long)
        ClearForColumnValue(AddressOf Initialize, TableName, ItemIdColumn, itemId)
    End Sub

    Public Sub Write(characterId As Long, equipSlot As Long, itemId As Long)
        ReplaceRecord(AddressOf Initialize, TableName, CharacterIdColumn, characterId, EquipSlotColumn, equipSlot, ItemIdColumn, itemId)
    End Sub

    Friend Sub ClearForCharacter(characterId As Long)
        ClearForColumnValue(AddressOf Initialize, TableName, CharacterIdColumn, characterId)
    End Sub

    Public Function Read(characterId As Long, equipSlot As Long) As Long?
        Initialize()
        Return ExecuteScalar(Of Long)(
            $"SELECT [{ItemIdColumn}] FROM [{TableName}] WHERE [{CharacterIdColumn}]=@{CharacterIdColumn} AND [{EquipSlotColumn}]=@{EquipSlotColumn};",
            MakeParameter($"@{CharacterIdColumn}", characterId),
            MakeParameter($"@{EquipSlotColumn}", equipSlot))
    End Function
End Module
