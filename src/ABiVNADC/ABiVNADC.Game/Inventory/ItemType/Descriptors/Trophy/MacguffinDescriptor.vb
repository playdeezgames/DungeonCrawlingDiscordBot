Friend Class MacguffinDescriptor
    Inherits ItemTypeDescriptor

    Sub New()
        MyBase.New()
        QuestTargetWeight = 1
        QuestTargetQuantityDice = "1d1"
        IsTrophy = True
    End Sub

    Public Overrides ReadOnly Property Name As String
        Get
            Return "macguffin"
        End Get
    End Property
End Class
