Friend Class GoldenEggDescriptor
    Inherits ItemTypeDescriptor
    Sub New()
        MyBase.New("golden egg", False, EquipSlot.None)
        CanSellGenerator = MakeBooleanGenerator(1, 1)
        SellPriceDice = "50d1+2d50"
        InventoryEncumbrance = 1
        IsTrophy = True
    End Sub
End Class
