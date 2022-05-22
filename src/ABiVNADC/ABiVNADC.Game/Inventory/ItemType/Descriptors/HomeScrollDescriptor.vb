Friend Class HomeScrollDescriptor
    Inherits ItemTypeDescriptor
    Sub New()
        MyBase.New
        Name = "home scroll"
        SpawnCount = Function(difficulty, locationCount) "0d1"
        CanBuyGenerator = MakeBooleanGenerator(3, 1)
        BuyPriceDice = "50d1+2d50"
        InventoryEncumbrance = 0
        Aliases = New List(Of String) From {"hs", "scroll"}
    End Sub
End Class
