Public Module CharacterInventoryData
    Friend Const TableName = "CharacterInventories"
    Friend Const CharacterIdColumn = CharacterData.CharacterIdColumn
    Friend Const InventoryIdColumn = InventoryData.InventoryIdColumn
    Friend Sub Initialize()
        CharacterData.Initialize()
        InventoryData.Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{CharacterIdColumn}] INT NOT NULL UNIQUE,
                [{InventoryIdColumn}] INT NOT NULL,
                FOREIGN KEY ([{CharacterIdColumn}]) REFERENCES [{CharacterData.TableName}]([{CharacterData.CharacterIdColumn}]),
                FOREIGN KEY ([{InventoryIdColumn}]) REFERENCES [{InventoryData.TableName}]([{InventoryData.InventoryIdColumn}])
            );")
    End Sub

    Public Sub Write(CharacterId As Long, inventoryId As Long)
        ReplaceRecord(AddressOf Initialize, TableName, CharacterIdColumn, CharacterId, InventoryIdColumn, inventoryId)
    End Sub

    Friend Sub ClearForCharacter(characterId As Long)
        ClearForColumnValue(AddressOf Initialize, TableName, CharacterIdColumn, characterId)
    End Sub

    Public Function ReadForCharacter(CharacterId As Long) As Long?
        Initialize()
        Return ReadColumnValue(Of Long)(AddressOf Initialize, TableName, CharacterIdColumn, CharacterId, InventoryIdColumn)
    End Function
End Module
