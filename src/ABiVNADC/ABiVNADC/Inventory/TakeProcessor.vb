Module TakeProcessor
    Friend Function Run(player As Player, tokens As IEnumerable(Of String)) As String
        If Not tokens.Any Then
            Return "Take what?"
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
        Dim itemStacks = location.Inventory.StackedItems
        If Not itemStacks.ContainsKey(itemType) Then
            Return $"There ain't any `{itemTypeName}` in sight."
        End If
        Dim item = itemStacks(itemType).First
        character.Inventory.Add(item)
        Return $"{character.Name} picks up {itemType.Name}"
    End Function
End Module
