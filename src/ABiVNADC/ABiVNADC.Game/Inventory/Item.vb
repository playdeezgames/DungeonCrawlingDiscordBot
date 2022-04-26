Public Class Item
    ReadOnly Property Id As Long
    Sub New(itemId As Long)
        Id = itemId
    End Sub
    Shared Function Create(itemType As ItemType) As Item
        Return New Item(ItemData.Create(itemType))
    End Function

    ReadOnly Property ItemType As ItemType
        Get
            Return CType(ItemData.ReadItemType(Id).Value, ItemType)
        End Get
    End Property

    Friend Sub Destroy()
        ItemData.Clear(Id)
    End Sub

    Friend Shared Function FromId(itemId As Long?) As Item
        Return If(itemId.HasValue, New Item(itemId.Value), Nothing)
    End Function
End Class
