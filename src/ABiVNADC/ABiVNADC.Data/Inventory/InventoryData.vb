Public Module InventoryData
    Friend Const TableName = "Inventories"
    Friend Const InventoryIdColumn = "InventoryId"
    Friend Sub Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]([{InventoryIdColumn}] INTEGER PRIMARY KEY AUTOINCREMENT);")
    End Sub
    Public Function Create() As Long
        Return CreateRecord(AddressOf Initialize, TableName)
    End Function

    Public Sub ClearOrphans()
        Initialize()
        Dim orphans =
            ExecuteReader(Of Long)(
                Function(reader) CLng(reader(InventoryIdColumn)),
                $"SELECT i.[{InventoryIdColumn}]
                FROM [{TableName}] i 
                LEFT JOIN [{CharacterInventoryData.TableName}] c 
                ON i.[{InventoryIdColumn}]=c.[{CharacterInventoryData.InventoryIdColumn}] 
                LEFT JOIN [{LocationInventoryData.TableName}] l 
                ON i.[{InventoryIdColumn}]=l.[{LocationInventoryData.InventoryIdColumn}] 
                WHERE 
                    c.[{CharacterInventoryData.InventoryIdColumn}] IS NULL AND 
                    l.[{LocationInventoryData.InventoryIdColumn}] IS NULL;")
        For Each orphan In orphans
            ClearForColumnValue(AddressOf Initialize, TableName, InventoryIdColumn, orphan)
        Next
    End Sub
End Module
