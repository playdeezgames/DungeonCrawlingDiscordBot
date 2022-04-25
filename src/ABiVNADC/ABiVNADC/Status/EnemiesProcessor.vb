Module EnemiesProcessor
    Friend Function Run(player As Player, tokens As IEnumerable(Of String)) As String
        If tokens.Any Then
            Return "The command is simpley `enemies`."
        End If
        If Not player.HasCharacter Then
            Return "You have no current character."
        End If
        If Not player.InCombat Then
            Return $"{player.Character.FullName} is not currently in combat."
        End If
        Dim builder As New StringBuilder
        builder.AppendLine($"{player.Character.FullName} currently faces:")
        For Each enemy In player.Character.Location.Enemies(player.Character)
            builder.AppendLine($"{enemy.FullName} (health:{enemy.Health}/{enemy.MaximumHealth})")
        Next
        Return builder.ToString
    End Function
End Module
