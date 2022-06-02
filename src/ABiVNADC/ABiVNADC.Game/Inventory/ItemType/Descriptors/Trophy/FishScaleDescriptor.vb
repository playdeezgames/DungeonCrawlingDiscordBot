Friend Class FishScaleDescriptor
    Inherits ItemTypeDescriptor

    Sub New()
        MyBase.New()
        CanSellGenerator = MakeBooleanGenerator(1, 1)
        SellPriceDice = "20d1+2d20"
        IsTrophy = True
    End Sub

    Public Overrides Function GetName() As String
        Return "fish scale"
    End Function
End Class
