Friend Class GoldenEggDescriptor
    Inherits ItemTypeDescriptor

    Sub New()
        MyBase.New(EquipSlot.None)
        CanSellGenerator = MakeBooleanGenerator(1, 1)
        SellPriceDice = "50d1+2d50"
        InventoryEncumbrance = 1
        IsTrophy = True
    End Sub

    Public Overrides Function GetName() As String
        Return "golden egg"
    End Function
End Class
