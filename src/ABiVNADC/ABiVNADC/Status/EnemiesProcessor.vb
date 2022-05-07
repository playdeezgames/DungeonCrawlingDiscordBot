Module EnemiesProcessor
    Friend Function Run(player As Player, tokens As IEnumerable(Of String)) As String
        If tokens.Any Then
            Return "The command is simpley `enemies`."
        End If
        Return RequireCharacter(
            player,
            Function(character)
                If Not character.InCombat Then
                    Return $"{character.FullName} is not currently in combat."
                End If
                Dim builder As New StringBuilder
                builder.AppendLine($"{character.FullName} currently faces:")
                For Each enemy In character.Location.Enemies(character)
                    builder.AppendLine($"{enemy.FullName} (health:{enemy.Health}/{enemy.MaximumHealth})")
                Next
                Return builder.ToString
            End Function)
    End Function
End Module
