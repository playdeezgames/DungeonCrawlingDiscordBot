Module DropProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        If Not tokens.Any Then
            builder.AppendLine("Drop what?")
            Return
        End If
        Dim itemTypeName = String.Join(" "c, tokens)

        Dim itemType = ParseItemType(itemTypeName)
        If itemType = ItemType.None Then
            builder.AppendLine($"I don't know what a `{itemTypeName}` is.")
            Return
        End If

        Dim character = player.Character
        If character Is Nothing Then
            builder.AppendLine("You have no current character.")
            Return
        End If
        Dim location = character.Location
        If location Is Nothing Then
            builder.AppendLine($"{character.Name} is not in a dungeon.")
            Return
        End If
        Dim itemStacks = character.Inventory.StackedItems
        If Not itemStacks.ContainsKey(itemType) Then
            builder.AppendLine($"{character.Name} doesn't have any `{itemTypeName}`.")
            Return
        End If
        Dim item = itemStacks(itemType).First
        character.Location.Inventory.Add(item)
        builder.AppendLine($"{character.Name} drops {itemType.Name}")
    End Sub
End Module
