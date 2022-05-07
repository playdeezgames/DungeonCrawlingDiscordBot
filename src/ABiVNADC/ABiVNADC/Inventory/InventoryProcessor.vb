Module InventoryProcessor
    Friend Function Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String)) As String
        If tokens.Any Then
            Return "Just `inventory`, ok?"
        End If
        Dim character = player.Character
        If character Is Nothing Then
            Return "You have no current character!"
        End If
        Dim inventory = character.Inventory
        If inventory.IsEmpty Then
            Return $"{character.Name} has nothing in their inventory."
        End If
        Dim itemStacks = inventory.StackedItems
        Return $"{character.Name}'s Inventory: {String.Join(", ", itemStacks.Select(Function(entry) $"{entry.Key.Name}(x{entry.Value.Count})"))}"
    End Function
End Module
