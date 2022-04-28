Public Module ItemDepletionData
    Friend Const TableName = "ItemDepletions"
    Friend Const ItemIdColumn = ItemData.ItemIdColumn
    Friend Const DepletionColumn = "Depletion"
    Friend Sub Initialize()
        ItemData.Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{ItemIdColumn}] INT NOT NULL UNIQUE,
                [{DepletionColumn}] INT NOT NULL,
                FOREIGN KEY ([{ItemIdColumn}]) REFERENCES [{ItemData.TableName}]([{ItemData.ItemIdColumn}])
            );")
    End Sub
    Public Function Read(itemId As Long) As Long?
        Return ReadColumnValue(Of Long)(AddressOf Initialize, TableName, ItemIdColumn, itemId, DepletionColumn)
    End Function

    Sub Clear(itemId As Long)
        ClearForColumnValue(AddressOf Initialize, TableName, ItemIdColumn, itemId)
    End Sub

    Public Sub Write(itemId As Long, depletion As Long)
        Initialize()
        ExecuteNonQuery(
            $"REPLACE INTO [{TableName}]
            (
                [{ItemIdColumn}],
                [{DepletionColumn}]
            ) 
            VALUES
            (
                @{ItemIdColumn},
                @{DepletionColumn}
            );",
            MakeParameter($"@{ItemIdColumn}", itemId),
            MakeParameter($"@{DepletionColumn}", depletion))
    End Sub
End Module
