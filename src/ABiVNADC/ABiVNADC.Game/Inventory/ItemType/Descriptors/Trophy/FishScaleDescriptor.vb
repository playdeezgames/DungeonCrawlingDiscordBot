Friend Class FishScaleDescriptor
    Inherits ItemTypeDescriptor

    Sub New()
        MyBase.New()
        GenerateCanSell = MakeBooleanGenerator(1, 1)
        SellPriceDice = "20d1+2d20"
        IsTrophy = True
    End Sub

    Public Overrides ReadOnly Property Name As String
        Get
            Return "fish scale"
        End Get
    End Property
End Class
