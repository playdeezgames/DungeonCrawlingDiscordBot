Public Module ItemDepletionData
    Friend Const TableName = "ItemDepletions"
    Friend Const ItemIdColumn = ItemData.ItemIdColumn
    Friend Const DurabilityTypeColumn = "DurabilityType"
    Friend Const DepletionColumn = "Depletion"
    Friend Sub Initialize()
        ItemData.Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{ItemIdColumn}] INT NOT NULL,
                [{DurabilityTypeColumn}] INT NOT NULL,
                [{DepletionColumn}] INT NOT NULL,
                UNIQUE([{ItemIdColumn}],[{DurabilityTypeColumn}]),
                FOREIGN KEY ([{ItemIdColumn}]) REFERENCES [{ItemData.TableName}]([{ItemData.ItemIdColumn}])
            );")
    End Sub
    Public Function Read(itemId As Long, durabilityType As Long) As Long?
        Return ReadColumnValue(Of Long, Long, Long)(AddressOf Initialize, TableName, DepletionColumn, (ItemIdColumn, itemId), (DurabilityTypeColumn, durabilityType))
    End Function

    Sub ClearForItem(itemId As Long)
        ClearForColumnValue(AddressOf Initialize, TableName, (ItemIdColumn, itemId))
    End Sub

    Public Sub Write(itemId As Long, durabilityType As Long, depletion As Long)
        ReplaceRecord(
            AddressOf Initialize,
            TableName,
            (ItemIdColumn, itemId),
            (DurabilityTypeColumn, durabilityType),
            (DepletionColumn, depletion))
    End Sub
End Module
