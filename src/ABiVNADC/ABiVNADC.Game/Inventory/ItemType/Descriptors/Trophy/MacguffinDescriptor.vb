Friend Class MacguffinDescriptor
    Inherits ItemTypeDescriptor

    Sub New()
        MyBase.New()
        QuestTargetWeight = 1
        QuestTargetQuantityDice = "1d1"
        IsTrophy = True
    End Sub

    Public Overrides Function GetName() As String
        Return "macguffin"
    End Function
End Class
