Module EnemiesProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        If tokens.Any Then
            builder.AppendLine("The command is simpley `enemies`.")
            Return
        End If
        RequireCharacter(
            player,
            builder,
            Sub(character)
                If Not character.InCombat Then
                    builder.AppendLine($"{character.FullName} is not currently in combat.")
                    Return
                End If
                builder.AppendLine($"{character.FullName} currently faces:")
                For Each enemy In character.Location.Enemies(character)
                    builder.AppendLine($"{enemy.FullName} (health:{enemy.Health}/{enemy.MaximumHealth})")
                Next
            End Sub)
    End Sub
End Module
