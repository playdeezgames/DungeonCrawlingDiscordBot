Module UnequipProcessor
    Friend Function Run(player As Player, tokens As IEnumerable(Of String)) As String
        Dim character = player.Character
        If character Is Nothing Then
            Return "You don't have a current character."
        End If
        Dim equipSlotName = StitchTokens(tokens)
        Dim equipSlot = ParseEquipSlot(equipSlotName)
        If equipSlot = EquipSlot.None Then
            Return $"I don't know where yer `{equipSlotName}` is."
        End If
        Dim equipment = character.Equipment
        If Not equipment.ContainsKey(equipSlot) Then
            Return "You don't have anything equipped there."
        End If
        Dim item = equipment(equipSlot)
        Dim output As String = character.Unequip(item)
        Return DoCounterAttacks(player, output)
    End Function
End Module
