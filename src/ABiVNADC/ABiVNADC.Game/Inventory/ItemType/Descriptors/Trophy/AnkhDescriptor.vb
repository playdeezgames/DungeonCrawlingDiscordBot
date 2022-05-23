Friend Class AnkhDescriptor
    Inherits ItemTypeDescriptor
    Sub New()
        MyBase.New("ankh", False)
        CanSellGenerator = MakeBooleanGenerator(1, 1)
        SellPriceDice = "100d1+2d100"
        IsTrophy = True
    End Sub
End Class
