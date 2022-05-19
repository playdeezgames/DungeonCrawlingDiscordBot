Public Module ItemModifierData
    Friend Const TableName = "ItemModifiers"
    Friend Const ItemIdColumn = ItemData.ItemIdColumn
    Friend Const ModifierColumn = "Modifier"
    Friend Sub Initialize()
        ItemData.Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{ItemIdColumn}] INT NOT NULL UNIQUE,
                [{ModifierColumn}] INT NOT NULL,
                FOREIGN KEY ([{ItemIdColumn}]) REFERENCES [{ItemData.TableName}]([{ItemData.ItemIdColumn}])
            );")
    End Sub
    Public Sub Write(itemId As Long, modifierType As Long)
        ReplaceRecord(AddressOf Initialize, TableName, ItemIdColumn, itemId, ModifierColumn, modifierType)
    End Sub

    Friend Sub ClearForItem(itemId As Long)
        ClearForColumnValue(AddressOf Initialize, TableName, ItemIdColumn, itemId)
    End Sub
End Module
