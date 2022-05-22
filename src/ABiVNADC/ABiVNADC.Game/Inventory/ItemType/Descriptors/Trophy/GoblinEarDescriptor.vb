Friend Class GoblinEarDescriptor
    Inherits ItemTypeDescriptor
    Sub New()
        MyBase.New("goblin ear")
        SpawnCount = Function(difficulty, locationCount) "0d1"
        CanSellGenerator = MakeBooleanGenerator(1, 1)
        SellPriceDice = "2d1+2d2"
        IsTrophy = True
    End Sub
End Class
