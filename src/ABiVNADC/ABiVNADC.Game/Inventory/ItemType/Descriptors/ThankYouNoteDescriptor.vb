Friend Class ThankYouNoteDescriptor
    Inherits ItemTypeDescriptor
    Sub New()
        MyBase.New
        Name = "thank you note"
        QuestRewardWeight = 1
        QuestRewardQuantityDice = "1d1"
        InventoryEncumbrance = -5
    End Sub
End Class
