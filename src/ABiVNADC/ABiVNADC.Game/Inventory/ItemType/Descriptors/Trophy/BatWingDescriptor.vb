Friend Class BatWingDescriptor
    Inherits ItemTypeDescriptor

    Sub New()
        MyBase.New()
        GenerateCanSell = MakeBooleanGenerator(1, 1)
        SellPriceDice = "2d1+2d2"
        IsTrophy = True
    End Sub

    Public Overrides ReadOnly Property Name As String
        Get
            Return "bat wing"
        End Get
    End Property
End Class
