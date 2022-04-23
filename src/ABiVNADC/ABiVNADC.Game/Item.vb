Public Class Item
    ReadOnly Property Id As Long
    Sub New(itemId As Long)
        Id = itemId
    End Sub
    Shared Function Create(itemType As ItemType) As Item
        Return New Item(ItemData.Create(itemType))
    End Function
End Class
