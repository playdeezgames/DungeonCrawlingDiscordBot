Module TakeProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        If Not tokens.Any Then
            builder.AppendLine("Take what?")
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
        Dim itemTypeName = String.Join(" "c, tokens)
        If itemTypeName = AllText Then
            builder.AppendLine(HandleTakeAll(player, character, location))
            Return
        End If
        Dim itemType = ParseItemType(itemTypeName)
        If itemType = ItemType.None Then
            builder.AppendLine($"I don't know what a `{itemTypeName}` is.")
            Return
        End If
        Dim itemStacks = location.Inventory.StackedItems
        If Not itemStacks.ContainsKey(itemType) Then
            builder.AppendLine($"There ain't any `{itemTypeName}` in sight.")
            Return
        End If
        Dim item = itemStacks(itemType).First
        character.Inventory.Add(item)
        builder.AppendLine(DoCounterAttacks(character, $"{character.FullName} picks up {itemType.Name}"))
    End Sub

    Private Function HandleTakeAll(player As Player, character As Character, location As Location) As String
        If location.Inventory.IsEmpty Then
            Return "There's nothing to take!"
        End If
        For Each item In location.Inventory.Items
            character.Inventory.Add(item)
        Next
        Return DoCounterAttacks(character, $"{character.FullName} takes everything.
")
    End Function
End Module
