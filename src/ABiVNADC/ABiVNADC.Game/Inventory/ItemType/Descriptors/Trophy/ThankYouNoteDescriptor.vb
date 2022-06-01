Friend Class ThankYouNoteDescriptor
    Inherits ItemTypeDescriptor

    Sub New()
        MyBase.New(False, EquipSlot.None)
        QuestRewardWeight = 1
        QuestRewardQuantityDice = "1d1"
        InventoryEncumbrance = -5
    End Sub

    Public Overrides Function GetName() As String
        Return "thank you note"
    End Function
End Class
