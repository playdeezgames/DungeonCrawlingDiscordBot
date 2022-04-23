Module DropProcessor
    Friend Function Run(player As Player, tokens As IEnumerable(Of String)) As String
        If Not tokens.Any Then
            Return "Drop what?"
        End If
        Dim itemTypeName = String.Join(" "c, tokens)
        Dim itemType = AllItemTypes.SingleOrDefault(Function(x) x.Name = itemTypeName)
        If itemType = ItemType.None Then
            Return $"I don't know what a `{itemTypeName}` is."
        End If
        Dim character = player.Character
        If character Is Nothing Then
            Return "You have no current character."
        End If
        Dim location = character.Location
        If location Is Nothing Then
            Return $"{character.Name} is not in a dungeon."
        End If
        Dim itemStacks = character.Inventory.StackedItems
        If Not itemStacks.ContainsKey(itemType) Then
            Return $"{character.Name} doesn't have any `{itemTypeName}`."
        End If
        Dim item = itemStacks(itemType).First
        character.Location.Inventory.Add(item)
        Return $"{character.Name} drops {itemType.Name}"
    End Function
End Module
