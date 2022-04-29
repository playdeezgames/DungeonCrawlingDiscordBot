Module RunProcessor
    Friend Function Run(player As Player, tokens As IEnumerable(Of String)) As String
        If tokens.Any Then
            Return "The command is just `run`."
        End If
        If Not player.HasCharacter Then
            Return "You have no current charcter."
        End If
        Dim character = player.Character
        If Not character.InCombat Then
            Return $"{character.FullName} is not in combat."
        End If
        If player.Run() Then
            Dim canvas = DrawPOV(player)
            Return $"{character.FullName} runs!
```{canvas.Output}```"
        End If
        Return DoCounterAttacks(player, $"{character.FullName} could not get away.
")
    End Function
End Module
