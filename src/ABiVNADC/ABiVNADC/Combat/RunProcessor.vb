Module RunProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        If tokens.Any Then
            builder.AppendLine("The command is just `run`.")
            Return
        End If
        builder.AppendLine(RequireCharacter(
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
            End Function))
    End Sub
End Module
