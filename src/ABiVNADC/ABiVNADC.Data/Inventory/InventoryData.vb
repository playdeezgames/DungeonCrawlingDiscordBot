Public Module InventoryData
    Friend Const TableName = "Inventories"
    Friend Const InventoryIdColumn = "InventoryId"
    Friend Sub Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]([{InventoryIdColumn}] INTEGER PRIMARY KEY AUTOINCREMENT);")
    End Sub
    Public Function Create() As Long
        Initialize()
        ExecuteNonQuery($"INSERT INTO [{TableName}] DEFAULT VALUES;")
        Return LastInsertRowId
    End Function

    Public Sub ClearOrphans()
        Initialize()
        ExecuteNonQuery(
            $"DELETE i 
            FROM [{TableName}] i 
            LEFT JOIN [{CharacterInventoryData.TableName}] c 
            ON i.[{InventoryIdColumn}]=c.[{CharacterInventoryData.InventoryIdColumn}] 
            LEFT JOIN [{LocationInventoryData.TableName}] l 
            ON i.[{InventoryIdColumn}]=l.[{LocationInventoryData.InventoryIdColumn}] 
            WHERE 
                c.[{CharacterInventoryData.InventoryIdColumn}] IS NULL AND 
                l.[{LocationInventoryData.InventoryIdColumn}] IS NULL;")
    End Sub
End Module
