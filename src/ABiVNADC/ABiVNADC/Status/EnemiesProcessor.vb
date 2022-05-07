Module EnemiesProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        If tokens.Any Then
            builder.AppendLine("The command is simpley `enemies`.")
            Return
        End If
        builder.AppendLine(RequireCharacter(
            player,
            Function(character)
                If Not character.InCombat Then
                    Return $"{character.FullName} is not currently in combat."
                End If
                builder.AppendLine($"{character.FullName} currently faces:")
                For Each enemy In character.Location.Enemies(character)
                    builder.AppendLine($"{enemy.FullName} (health:{enemy.Health}/{enemy.MaximumHealth})")
                Next
                Return builder.ToString
            End Function))
    End Sub
End Module
