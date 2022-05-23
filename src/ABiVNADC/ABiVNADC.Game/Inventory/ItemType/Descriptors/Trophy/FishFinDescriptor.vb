Friend Class FishFinDescriptor
    Inherits ItemTypeDescriptor
    Sub New()
        MyBase.New("fish fin", False)
        CanSellGenerator = MakeBooleanGenerator(1, 1)
        SellPriceDice = "40d1+2d40"
        IsTrophy = True
    End Sub

End Class
