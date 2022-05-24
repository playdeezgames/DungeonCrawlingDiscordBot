Friend Class BatWingDescriptor
    Inherits ItemTypeDescriptor
    Sub New()
        MyBase.New("bat wing", False)
        CanSellGenerator = MakeBooleanGenerator(1, 1)
        SellPriceDice = "2d1+2d2"
        IsTrophy = True
    End Sub
End Class
