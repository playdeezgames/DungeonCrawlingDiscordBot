Friend Class FishScaleDescriptor
    Inherits ItemTypeDescriptor
    Sub New()
        MyBase.New("fish scale")
        SpawnCount = Function(difficulty, locationCount) "0d1"
        CanSellGenerator = MakeBooleanGenerator(1, 1)
        SellPriceDice = "20d1+2d20"
        IsTrophy = True
    End Sub

End Class
