Friend Class GoldenEggDescriptor
    Inherits ItemTypeDescriptor

    Sub New()
        MyBase.New()
        CanSellGenerator = MakeBooleanGenerator(1, 1)
        SellPriceDice = "50d1+2d50"
        InventoryEncumbrance = 1
        IsTrophy = True
    End Sub

    Public Overrides ReadOnly Property Name As String
        Get
            Return "golden egg"
        End Get
    End Property
End Class
