Module GroundProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        If tokens.Any Then
            builder.AppendLine("Just `ground`, ok?")
            Return
        End If
        Dim character = player.Character
        If character Is Nothing Then
            builder.AppendLine("You have no current character!")
            Return
        End If
        Dim location = character.Location
        If location Is Nothing Then
            builder.AppendLine("Yer character is not in a dungeon.")
            Return
        End If
        Dim inventory = location.Inventory
        If inventory.IsEmpty Then
            builder.AppendLine("There is nothing on the ground.")
            Return
        End If
        Dim itemStacks = inventory.StackedItems
        builder.AppendLine($"Items on the ground: {String.Join(", ", itemStacks.Select(Function(entry) $"{entry.Key.Name}(x{entry.Value.Count})"))}")
    End Sub
End Module
