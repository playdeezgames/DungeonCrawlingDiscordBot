Friend Class GoosePoopDescriptor
    Inherits ItemTypeDescriptor
    Sub New()
        MyBase.New("goose poop", False, EquipSlot.None)
        InventoryEncumbrance = 1
        Aliases = New List(Of String) From {"poop"}
    End Sub
End Class
