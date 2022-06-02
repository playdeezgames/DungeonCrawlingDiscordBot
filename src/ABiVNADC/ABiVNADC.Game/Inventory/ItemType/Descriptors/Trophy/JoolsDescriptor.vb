Friend Class JoolsDescriptor
    Inherits ItemTypeDescriptor

    Sub New()
        MyBase.New()
        CanBuyGenerator = MakeBooleanGenerator(0, 1)
        CanSellGenerator = MakeBooleanGenerator(0, 1)
        BuyPriceDice = "100d1"
        SellPriceDice = "95d1"
        InventoryEncumbrance = 1
        Aliases = New List(Of String) From {"j", "$"}
    End Sub

    Public Overrides ReadOnly Property Name As String
        Get
            Return "jools"
        End Get
    End Property
End Class
