Friend Class GoblinEarDescriptor
    Inherits ItemTypeDescriptor

    Sub New()
        MyBase.New()
        GenerateCanSell = MakeBooleanGenerator(1, 1)
        SellPriceDice = "2d1+2d2"
        IsTrophy = True
    End Sub

    Public Overrides ReadOnly Property Name As String
        Get
            Return "goblin ear"
        End Get
    End Property
End Class
