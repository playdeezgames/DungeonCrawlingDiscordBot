Module GroundProcessor
    Friend Function Run(player As Player, tokens As IEnumerable(Of String)) As String
        If tokens.Any Then
            Return "Just `ground`, ok?"
        End If
        Dim character = player.Character
        If character Is Nothing Then
            Return "You have no current character!"
        End If
        Dim location = character.Location
        If location Is Nothing Then
            Return "Yer character is not in a dungeon."
        End If
        Dim inventory = location.Inventory
        If inventory.IsEmpty Then
            Return "There is nothing on the ground."
        End If
        Dim itemStacks = inventory.StackedItems
        Return $"Items on the ground: {String.Join(", ", itemStacks.Select(Function(entry) $"{entry.Key.Name}(x{entry.Value.Count})"))}"
    End Function
End Module
