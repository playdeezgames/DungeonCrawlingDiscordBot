Public Class Inventory
    ReadOnly Property Id As Long
    Sub New(inventoryId As Long)
        Id = inventoryId
    End Sub
    Sub Add(item As Item)
        InventoryItemData.Write(Id, item.Id)
    End Sub
    ReadOnly Property IsEmpty As Boolean
        Get
            Return InventoryItemData.ReadCountForInventory(Id) = 0
        End Get
    End Property
End Class
