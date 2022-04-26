Module EquipProcessor
    Friend Function Run(player As Player, tokens As IEnumerable(Of String)) As String
        If Not tokens.Any Then
            Return "The command is `equip (item)`."
        End If
        If Not player.HasCharacter Then
            Return "You have no current character."
        End If
        Dim itemTypeName = String.Join(" "c, tokens)
        Dim itemType = ParseItemType(itemTypeName)
        If itemType = ItemType.None Then
            Return $"I don't know what a `{itemTypeName}` is."
        End If
        Dim output = player.Character.Equip(itemType)
        Return DoCounterAttacks(player, output)
    End Function
End Module
