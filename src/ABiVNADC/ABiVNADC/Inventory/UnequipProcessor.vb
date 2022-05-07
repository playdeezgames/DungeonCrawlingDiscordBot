Module UnequipProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        Dim character = player.Character
        If character Is Nothing Then
            builder.AppendLine("You don't have a current character.")
            Return
        End If
        Dim itemTypeName = StitchTokens(tokens)
        Dim itemType = ParseItemType(itemTypeName)
        Dim equipSlots = character.Equipment.Where(Function(x) x.Value.ItemType = itemType)
        If Not equipSlots.Any Then
            builder.AppendLine($"You don't have any `{itemTypeName}` equipped.")
        End If
        Dim output As String = character.Unequip(equipSlots.First.Value)
        builder.AppendLine(DoCounterAttacks(character, output))
    End Sub
End Module
