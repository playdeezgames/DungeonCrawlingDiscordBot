Module EquipProcessor
    Friend Function Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String)) As String
        If Not tokens.Any Then
            Return "The command is `equip (item)`."
        End If
        Return RequireCharacter(
            player,
            Function(character)
                Dim itemTypeName = String.Join(" "c, tokens)
                Dim itemType = ParseItemType(itemTypeName)
                If itemType = ItemType.None Then
                    Return $"I don't know what a `{itemTypeName}` is."
                End If
                Dim output = character.Equip(itemType)
                Return DoCounterAttacks(character, output)
            End Function)
    End Function
End Module
