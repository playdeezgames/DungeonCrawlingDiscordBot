Module TakeProcessor
    Friend Function Run(player As Player, tokens As IEnumerable(Of String)) As String
        If Not tokens.Any Then
            Return "Take what?"
        End If
        Dim character = player.Character
        If character Is Nothing Then
            Return "You have no current character."
        End If
        Dim location = character.Location
        If location Is Nothing Then
            Return $"{character.Name} is not in a dungeon."
        End If
        Dim itemTypeName = String.Join(" "c, tokens)
        If itemTypeName = AllText Then
            Return HandleTakeAll(player, character, location)
        End If
        Dim itemType = AllItemTypes.SingleOrDefault(Function(x) x.Name = itemTypeName)
        If itemType = ItemType.None Then
            Return $"I don't know what a `{itemTypeName}` is."
        End If
        Dim itemStacks = Location.Inventory.StackedItems
        If Not itemStacks.ContainsKey(itemType) Then
            Return $"There ain't any `{itemTypeName}` in sight."
        End If
        Dim item = itemStacks(itemType).First
        character.Inventory.Add(item)
        Dim builder As New StringBuilder
        builder.AppendLine($"{character.FullName} picks up {itemType.Name}")
        DoCounterAttacks(player, character, builder)
        Return builder.ToString
    End Function

    Private Function HandleTakeAll(player As Player, character As Character, location As Location) As String
        If location.Inventory.IsEmpty Then
            Return "There's nothing to take!"
        End If
        For Each item In location.Inventory.Items
            character.Inventory.Add(item)
        Next
        Dim builder As New StringBuilder
        builder.AppendLine($"{character.FullName} takes everything.")
        DoCounterAttacks(player, character, builder)
        Return builder.ToString
    End Function
End Module
