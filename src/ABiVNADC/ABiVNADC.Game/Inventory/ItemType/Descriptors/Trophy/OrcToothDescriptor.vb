Friend Class OrcToothDescriptor
    Inherits ItemTypeDescriptor

    Sub New()
        MyBase.New()
        CanSellGenerator = MakeBooleanGenerator(1, 1)
        SellPriceDice = "5d1+2d5"
        IsTrophy = True
    End Sub

    Public Overrides Function GetName() As String
        Return "orc tooth"
    End Function
End Class
