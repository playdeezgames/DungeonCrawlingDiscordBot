Friend Class RatTailDescriptor
    Inherits ItemTypeDescriptor

    Sub New()
        MyBase.New()
        CanSellGenerator = MakeBooleanGenerator(1, 1)
        SellPriceDice = "2d1+2d2"
        IsTrophy = True
    End Sub

    Public Overrides ReadOnly Property Name As String
        Get
            Return "rat tail"
        End Get
    End Property
End Class
