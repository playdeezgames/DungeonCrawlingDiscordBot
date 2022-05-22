Friend Class ZombieTaintDescriptor
    Inherits ItemTypeDescriptor
    Sub New()
        MyBase.New("zombie taint")
        SpawnCount = Function(difficulty, locationCount) "0d1"
        CanSellGenerator = MakeBooleanGenerator(1, 1)
        SellPriceDice = "30d1+2d30"
        IsTrophy = True
    End Sub
End Class
