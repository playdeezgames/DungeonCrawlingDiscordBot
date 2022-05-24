Friend Class RatTailDescriptor
    Inherits ItemTypeDescriptor
    Sub New()
        MyBase.New("rat tail", False)
        CanSellGenerator = MakeBooleanGenerator(1, 1)
        SellPriceDice = "2d1+2d2"
        IsTrophy = True
    End Sub
End Class
