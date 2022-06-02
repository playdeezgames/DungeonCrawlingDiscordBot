Friend Class ZombieTaintDescriptor
    Inherits ItemTypeDescriptor

    Sub New()
        MyBase.New()
        CanSellGenerator = MakeBooleanGenerator(1, 1)
        SellPriceDice = "30d1+2d30"
        IsTrophy = True
    End Sub

    Public Overrides Function GetName() As String
        Return "zombie taint"
    End Function
End Class
