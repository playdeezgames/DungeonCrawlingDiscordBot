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

    ReadOnly Property InventoryEncumbrance As Long
        Get
            Return Items.Sum(Function(x) x.InventoryEncumbrance)
        End Get
    End Property

    ReadOnly Property Items As IEnumerable(Of Item)
        Get
            Return InventoryItemData.ReadForInventory(Id).Select(Function(id) New Item(id))
        End Get
    End Property

    ReadOnly Property StackedItems As Dictionary(Of ItemType, IEnumerable(Of Item))
        Get
            Dim itemStacks = Items.GroupBy(Function(x) x.ItemType)
            Dim result As New Dictionary(Of ItemType, IEnumerable(Of Item))
            For Each itemStack In itemStacks
                result(itemStack.Key) = itemStack
            Next
            Return result
        End Get
    End Property

    Function HasItem(itemType As ItemType) As Boolean
        Return Items.Any(Function(x) x.ItemType = itemType)
    End Function

    Friend Sub Remove(item As Item)
        InventoryItemData.ClearForItem(item.Id)
    End Sub
End Class
