Module EquipProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        If Not tokens.Any Then
            builder.AppendLine("The command is `equip (item)`.")
            Return
        End If
        RequireCharacter(
            player,
            builder,
            Sub(character)
                Dim itemTypeName = String.Join(" "c, tokens)
                Dim itemType = ParseItemType(itemTypeName)
                If itemType = ItemType.None Then
                    builder.AppendLine($"I don't know what a `{itemTypeName}` is.")
                    Return
                End If
                Dim output = character.Equip(itemType)
                builder.AppendLine(DoCounterAttacks(character, output))
            End Sub)
    End Sub
End Module
