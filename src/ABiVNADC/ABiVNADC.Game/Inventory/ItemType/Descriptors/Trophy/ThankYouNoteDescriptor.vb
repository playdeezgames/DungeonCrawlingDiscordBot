Friend Class ThankYouNoteDescriptor
    Inherits ItemTypeDescriptor
    Sub New()
        MyBase.New("thank you note", False, EquipSlot.None)
        QuestRewardWeight = 1
        QuestRewardQuantityDice = "1d1"
        InventoryEncumbrance = -5
    End Sub
End Class
