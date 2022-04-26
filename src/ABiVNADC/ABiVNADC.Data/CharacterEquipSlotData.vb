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
    Public Sub ClearForItem(itemId As Long)
        ClearForColumnValue(AddressOf Initialize, TableName, ItemIdColumn, itemId)
    End Sub

    Public Sub Write(characterId As Long, equipSlot As Long, itemId As Long)
        Initialize()
        ExecuteNonQuery(
            $"REPLACE INTO [{TableName}]([{CharacterIdColumn}],[{EquipSlotColumn}],[{ItemIdColumn}]) VALUES(@{CharacterIdColumn},@{EquipSlotColumn},@{ItemIdColumn});",
            MakeParameter($"@{CharacterIdColumn}", characterId),
            MakeParameter($"@{EquipSlotColumn}", equipSlot),
            MakeParameter($"@{ItemIdColumn}", itemId))
    End Sub

    Public Function Read(characterId As Long, equipSlot As Long) As Long?
        Initialize()
        Return ExecuteScalar(Of Long)(
            $"SELECT [{ItemIdColumn}] FROM [{TableName}] WHERE [{CharacterIdColumn}]=@{CharacterIdColumn} AND [{EquipSlotColumn}]=@{EquipSlotColumn};",
            MakeParameter($"@{CharacterIdColumn}", characterId),
            MakeParameter($"@{EquipSlotColumn}", equipSlot))
    End Function
End Module
