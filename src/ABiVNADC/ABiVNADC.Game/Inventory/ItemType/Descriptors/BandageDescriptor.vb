Friend Class BandageDescriptor
    Inherits ItemTypeDescriptor
    Sub New()
        MyBase.New
        Name = "bandage"
        SpawnCount = Function(difficulty, locationCount) "0d1"
        CanSellGenerator = MakeBooleanGenerator(1, 1)
        SellPriceDice = "10d1+2d10"
        IsTrophy = True
    End Sub
End Class
