﻿Public Module LocationInventoryData
    Friend Const TableName = "LocationInventories"
    Friend Const LocationIdColumn = LocationData.LocationIdColumn
    Friend Const InventoryIdColumn = InventoryData.InventoryIdColumn
    Friend Sub Initialize()
        LocationData.Initialize()
        InventoryData.Initialize()
        ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{LocationIdColumn}] INT NOT NULL UNIQUE,
                [{InventoryIdColumn}] INT NOT NULL,
                FOREIGN KEY ([{LocationIdColumn}]) REFERENCES [{LocationData.TableName}]([{LocationData.LocationIdColumn}]),
                FOREIGN KEY ([{InventoryIdColumn}]) REFERENCES [{InventoryData.TableName}]([{InventoryData.InventoryIdColumn}])
            );")
    End Sub

    Public Sub Write(locationId As Long, inventoryId As Long)
        Initialize()
        ExecuteNonQuery(
            $"REPLACE INTO [{TableName}]
            (
                [{LocationIdColumn}],
                [{InventoryIdColumn}]
            ) 
            VALUES 
            (
                @{LocationIdColumn},
                @{InventoryIdColumn}
            );",
            MakeParameter($"@{LocationIdColumn}", locationId),
            MakeParameter($"@{InventoryIdColumn}", inventoryId))
    End Sub

    Public Function ReadForLocation(locationId As Long) As Long?
        Initialize()
        Return ReadColumnValue(Of Long)(AddressOf Initialize, TableName, LocationIdColumn, locationId, InventoryIdColumn)
    End Function
End Module