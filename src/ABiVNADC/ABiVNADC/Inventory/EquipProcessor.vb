Module EquipProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        If Not tokens.Any Then
            builder.AppendLine("The command is `equip (item)`.")
            Return
        End If
        builder.AppendLine(RequireCharacter(
            player,
            Function(character)
                Dim itemTypeName = String.Join(" "c, tokens)
                Dim itemType = ParseItemType(itemTypeName)
                If itemType = ItemType.None Then
                    Return $"I don't know what a `{itemTypeName}` is."
                End If
                Dim output = character.Equip(itemType)
                Return DoCounterAttacks(character, output)
            End Function))
    End Sub
End Module
