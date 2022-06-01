Friend Class GooseFeatherDescriptor
    Inherits ItemTypeDescriptor
    Sub New()
        MyBase.New("goose feather", False, EquipSlot.None)
        CanSellGenerator = MakeBooleanGenerator(1, 1)
        SellPriceDice = "1d3"
        InventoryEncumbrance = 1
        Aliases = New List(Of String) From {"feather"}
        IsTrophy = True
    End Sub
End Class
