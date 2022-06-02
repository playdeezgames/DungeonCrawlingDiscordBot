Public Class AnkhDescriptor
    Inherits ItemTypeDescriptor
    Sub New()
        MyBase.New()
        GenerateCanSell = MakeBooleanGenerator(1, 1)
        SellPriceDice = "100d1+2d100"
        IsTrophy = True
    End Sub

    Public Overrides ReadOnly Property Name As String
        Get
            Return "ankh"
        End Get
    End Property
End Class
