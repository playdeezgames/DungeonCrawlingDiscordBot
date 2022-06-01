Friend Class MacguffinDescriptor
    Inherits ItemTypeDescriptor
    Sub New()
        MyBase.New("macguffin", False, EquipSlot.None)
        QuestTargetWeight = 1
        QuestTargetQuantityDice = "1d1"
        IsTrophy = True
    End Sub

End Class
