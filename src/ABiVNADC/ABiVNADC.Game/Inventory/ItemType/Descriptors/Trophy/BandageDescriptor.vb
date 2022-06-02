Friend Class BandageDescriptor
    Inherits ItemTypeDescriptor

    Sub New()
        MyBase.New()
        CanSellGenerator = MakeBooleanGenerator(1, 1)
        SellPriceDice = "10d1+2d10"
        IsTrophy = True
    End Sub

    Public Overrides ReadOnly Property Name As String
        Get
            Return "bandage"
        End Get
    End Property
End Class
