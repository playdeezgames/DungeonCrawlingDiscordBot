Friend Class BandageDescriptor
    Inherits ItemTypeDescriptor

    Sub New()
        MyBase.New()
        CanSellGenerator = MakeBooleanGenerator(1, 1)
        SellPriceDice = "10d1+2d10"
        IsTrophy = True
    End Sub

    Public Overrides Function GetName() As String
        Return "bandage"
    End Function
End Class
