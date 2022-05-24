Friend Class SnakeFangDescriptor
    Inherits ItemTypeDescriptor
    Sub New()
        MyBase.New("snake fang", False)
        CanSellGenerator = MakeBooleanGenerator(1, 1)
        SellPriceDice = "3d1+2d3"
        IsTrophy = True
    End Sub
End Class
