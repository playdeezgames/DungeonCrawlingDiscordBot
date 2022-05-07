Module Utility
    Sub RequireCharacter(player As Player, builder As StringBuilder, handler As Action(Of Character))
        If Not player.HasCharacter Then
            builder.AppendLine("You have no current character.")
            Return
        End If
        handler(player.Character)
    End Sub

    Sub RequireLocation(character As Character, builder As StringBuilder, handler As Action(Of Location))
        If Not character.HasLocation Then
            builder.AppendLine($"{character.FullName} is not in a dungeon!")
        End If
        handler(character.Location)
    End Sub

    Sub RequireNoTokens(tokens As IEnumerable(Of String), commandName As String, builder As StringBuilder, handler As Action)
        If tokens.Any Then
            builder.AppendLine($"Round here, just `{commandName}` is the command. I'm picky.")
            Return
        End If
        handler()
    End Sub

    Sub RequireTokens(tokens As IEnumerable(Of String), errorMessage As String, builder As StringBuilder, handler As Action)
        If Not tokens.Any Then
            builder.Append(errorMessage)
            Return
        End If
        handler()
    End Sub

    Sub RequireItemType(tokens As IEnumerable(Of String), builder As StringBuilder, handler As Action(Of ItemType))
        Dim itemTypeName = StitchTokens(tokens)
        Dim itemType = ParseItemType(itemTypeName)
        If itemType = ItemType.None Then
            builder.AppendLine($"I don't know what a `{itemTypeName}` is.")
            Return
        End If
        handler(itemType)
    End Sub

    Sub RequireInCombat(character As Character, builder As StringBuilder, handler As Action)
        If Not character.InCombat Then
            builder.AppendLine($"{character.FullName} is not in combat.")
            Return
        End If
        handler()
    End Sub
End Module
