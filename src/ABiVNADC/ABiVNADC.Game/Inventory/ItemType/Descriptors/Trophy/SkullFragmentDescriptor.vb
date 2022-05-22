Friend Class SkullFragmentDescriptor
    Inherits ItemTypeDescriptor
    Sub New()
        MyBase.New("skull fragment", False)
        SpawnCount = Function(difficulty, locationCount) "0d1"
        CanSellGenerator = MakeBooleanGenerator(1, 1)
        SellPriceDice = "5d1+2d5"
        IsTrophy = True
    End Sub

End Class
