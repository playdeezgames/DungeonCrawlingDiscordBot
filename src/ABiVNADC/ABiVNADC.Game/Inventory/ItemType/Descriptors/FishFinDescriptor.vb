Friend Class FishFinDescriptor
    Inherits ItemTypeDescriptor
    Sub New()
        MyBase.New
        Name = "fish fin"
        SpawnCount = Function(difficulty, locationCount) "0d1"
        CanSellGenerator = MakeBooleanGenerator(1, 1)
        SellPriceDice = "40d1+2d40"
        IsTrophy = True
    End Sub

End Class
