Module InventoryProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        If tokens.Any Then
            builder.AppendLine("Just `inventory`, ok?")
            Return
        End If
        Dim character = player.Character
        If character Is Nothing Then
            builder.AppendLine("You have no current character!")
            Return
        End If
        Dim inventory = character.Inventory
        If inventory.IsEmpty Then
            builder.AppendLine($"{character.Name} has nothing in their inventory.")
            Return
        End If
        Dim itemStacks = inventory.StackedItems
        builder.AppendLine($"{character.Name}'s Inventory: {String.Join(", ", itemStacks.Select(Function(entry) $"{entry.Key.Name}(x{entry.Value.Count})"))}")
    End Sub
End Module
