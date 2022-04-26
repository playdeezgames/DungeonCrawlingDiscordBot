Module UseProcessor
    Friend Function Run(player As Player, tokens As IEnumerable(Of String)) As String
        If Not tokens.Any Then
            Return "Use what?"
        End If
        Dim itemTypeName = String.Join(" "c, tokens)
        Dim itemType = AllItemTypes.SingleOrDefault(Function(x) x.Name = itemTypeName)
        If itemType = ItemType.None Then
            Return $"I don't know what a `{itemTypeName}` is."
        End If
        If Not itemType.CanUse Then
            Return $"Cannot use `{itemTypeName}`."
        End If
        Dim character = player.Character
        If character Is Nothing Then
            Return "You have no current character."
        End If
        Dim itemStacks = character.Inventory.StackedItems
        If Not itemStacks.ContainsKey(itemType) Then
            Return $"{character.Name} doesn't have any `{itemTypeName}`."
        End If
        Dim item = itemStacks(itemType).First
        Dim builder As New StringBuilder
        builder.AppendLine(player.UseItem(item))
        If character.HasLocation Then
            DoCounterAttacks(player, character, builder)
        End If
        Return builder.ToString
    End Function
End Module
