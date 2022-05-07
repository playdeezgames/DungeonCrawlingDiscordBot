Module RunProcessor
    Friend Function Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String)) As String
        If tokens.Any Then
            Return "The command is just `run`."
        End If
        Return RequireCharacter(
            player,
            Function(character)
                If Not character.InCombat Then
                    Return $"{character.FullName} is not in combat."
                End If
                If player.Run() Then
                    Dim canvas = DrawPOV(player)
                    Return $"{character.FullName} runs!
```{canvas.Output}```"
                End If
                Return DoCounterAttacks(character, $"{character.FullName} could not get away.
")
            End Function)
    End Function
End Module
