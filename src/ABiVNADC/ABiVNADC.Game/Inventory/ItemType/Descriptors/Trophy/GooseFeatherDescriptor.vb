Friend Class GooseFeatherDescriptor
    Inherits ItemTypeDescriptor

    Sub New()
        MyBase.New()
        CanSellGenerator = MakeBooleanGenerator(1, 1)
        SellPriceDice = "1d3"
        InventoryEncumbrance = 1
        Aliases = New List(Of String) From {"feather"}
        IsTrophy = True
    End Sub

    Public Overrides ReadOnly Property Name As String
        Get
            Return "goose feather"
        End Get
    End Property
End Class
