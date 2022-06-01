Friend Class FishScaleDescriptor
    Inherits ItemTypeDescriptor
    Sub New()
        MyBase.New("fish scale", False, EquipSlot.None)
        CanSellGenerator = MakeBooleanGenerator(1, 1)
        SellPriceDice = "20d1+2d20"
        IsTrophy = True
    End Sub

End Class
