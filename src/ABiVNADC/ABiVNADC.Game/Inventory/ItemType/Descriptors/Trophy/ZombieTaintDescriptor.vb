Friend Class ZombieTaintDescriptor
    Inherits ItemTypeDescriptor

    Sub New()
        MyBase.New()
        CanSellGenerator = MakeBooleanGenerator(1, 1)
        SellPriceDice = "30d1+2d30"
        IsTrophy = True
    End Sub

    Public Overrides ReadOnly Property Name As String
        Get
            Return "zombie taint"
        End Get
    End Property
End Class
