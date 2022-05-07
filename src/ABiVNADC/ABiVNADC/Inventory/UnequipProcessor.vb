Module UnequipProcessor
    Friend Function Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String)) As String
        Dim character = player.Character
        If character Is Nothing Then
            Return "You don't have a current character."
        End If
        Dim itemTypeName = StitchTokens(tokens)
        Dim itemType = ParseItemType(itemTypeName)
        Dim equipSlots = character.Equipment.Where(Function(x) x.Value.ItemType = itemType)
        If Not equipSlots.Any Then
            Return $"You don't have any `{itemTypeName}` equipped."
        End If
        Dim output As String = character.Unequip(equipSlots.First.Value)
        Return DoCounterAttacks(character, output)
    End Function
End Module
