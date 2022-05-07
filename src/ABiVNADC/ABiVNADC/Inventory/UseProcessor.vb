Module UseProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        If Not tokens.Any Then
            builder.AppendLine("Use what?")
            Return
        End If
        Dim itemTypeName = String.Join(" "c, tokens)
        Dim itemType = AllItemTypes.SingleOrDefault(Function(x) x.Name = itemTypeName)
        If itemType = ItemType.None Then
            builder.AppendLine($"I don't know what a `{itemTypeName}` is.")
            Return
        End If
        If Not itemType.CanUse Then
            builder.AppendLine($"Cannot use `{itemTypeName}`.")
            Return
        End If
        Dim character = player.Character
        If character Is Nothing Then
            builder.AppendLine("You have no current character.")
            Return
        End If
        Dim itemStacks = character.Inventory.StackedItems
        If Not itemStacks.ContainsKey(itemType) Then
            builder.AppendLine($"{character.Name} doesn't have any `{itemTypeName}`.")
            Return
        End If
        Dim item = itemStacks(itemType).First
        builder.AppendLine(player.UseItem(item))
        PerformCounterAttacks(character, builder)
    End Sub
End Module
