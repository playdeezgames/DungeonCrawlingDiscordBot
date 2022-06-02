Friend Class GooseFeatherDescriptor
    Inherits ItemTypeDescriptor

    Sub New()
        MyBase.New(EquipSlot.None)
        CanSellGenerator = MakeBooleanGenerator(1, 1)
        SellPriceDice = "1d3"
        InventoryEncumbrance = 1
        Aliases = New List(Of String) From {"feather"}
        IsTrophy = True
    End Sub

    Public Overrides Function GetName() As String
        Return "goose feather"
    End Function
End Class
