Public Module ItemModifierData
    Friend Const TableName = "ItemModifiers"
    Friend Const ItemIdColumn = ItemData.ItemIdColumn
    Friend Const ModifierColumn = "Modifier"
    Friend Const LevelColumn = "Level"
    Friend Sub Initialize()
        ItemData.Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{ItemIdColumn}] INT NOT NULL,
                [{ModifierColumn}] INT NOT NULL,
                [{LevelColumn}] INT NOT NULL,
                UNIQUE([{ItemIdColumn}],[{ModifierColumn}]),
                FOREIGN KEY ([{ItemIdColumn}]) REFERENCES [{ItemData.TableName}]([{ItemData.ItemIdColumn}])
            );")
    End Sub
    Public Sub Write(itemId As Long, modifierType As Long, level As Long)
        ReplaceRecord(AddressOf Initialize, TableName, ItemIdColumn, itemId, ModifierColumn, modifierType, LevelColumn, level)
    End Sub

    Friend Sub ClearForItem(itemId As Long)
        ClearForColumnValue(AddressOf Initialize, TableName, ItemIdColumn, itemId)
    End Sub

    Public Function ReadLevel(itemId As Long, modifierType As Long) As Long?
        Return ReadColumnValue(Of Long, Long, Long)(AddressOf Initialize, TableName, LevelColumn, (ItemIdColumn, itemId), (ModifierColumn, modifierType))
    End Function

    Public Function Read(itemId As Long) As Dictionary(Of Long, Long)
        Initialize()
        Dim entries = ExecuteReader(
            Function(reader) (CLng(reader(ModifierColumn)), CLng(reader(LevelColumn))),
            $"SELECT 
                [{ModifierColumn}],
                [{LevelColumn}] 
            FROM [{TableName}] 
            WHERE 
                [{ItemIdColumn}]=@{ItemIdColumn};",
            MakeParameter($"@{ItemIdColumn}", itemId))
        Return entries.ToDictionary(Of Long, Long)(Function(x) x.Item1, Function(x) x.Item2)
    End Function
End Module
