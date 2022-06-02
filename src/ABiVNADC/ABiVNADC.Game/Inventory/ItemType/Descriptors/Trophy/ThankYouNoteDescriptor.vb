Friend Class ThankYouNoteDescriptor
    Inherits ItemTypeDescriptor

    Sub New()
        MyBase.New()
        QuestRewardWeight = 1
        QuestRewardQuantityDice = "1d1"
        InventoryEncumbrance = -5
    End Sub

    Public Overrides ReadOnly Property Name As String
        Get
            Return "thank you note"
        End Get
    End Property
End Class
