Friend Class GoosePoopDescriptor
    Inherits ItemTypeDescriptor
    Sub New()
        MyBase.New("goose poop", False, EquipSlot.Weapon)
        InventoryEncumbrance = 1
        Aliases = New List(Of String) From {"poop"}
        IsTrophy = True
    End Sub
End Class
