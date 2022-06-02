Friend Class FishFinDescriptor
    Inherits ItemTypeDescriptor

    Sub New()
        MyBase.New()
        GenerateCanSell = MakeBooleanGenerator(1, 1)
        SellPriceDice = "40d1+2d40"
        IsTrophy = True
    End Sub

    Public Overrides ReadOnly Property Name As String
        Get
            Return "fish fin"
        End Get
    End Property
End Class
