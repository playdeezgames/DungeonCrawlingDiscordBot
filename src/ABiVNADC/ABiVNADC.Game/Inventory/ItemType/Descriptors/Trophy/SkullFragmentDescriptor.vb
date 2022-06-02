Friend Class SkullFragmentDescriptor
    Inherits ItemTypeDescriptor
    Sub New()
        MyBase.New()
        GenerateCanSell = MakeBooleanGenerator(1, 1)
        SellPriceDice = "5d1+2d5"
        IsTrophy = True
    End Sub

    Public Overrides ReadOnly Property Name As String
        Get
            Return "skull fragment"
        End Get
    End Property
End Class
