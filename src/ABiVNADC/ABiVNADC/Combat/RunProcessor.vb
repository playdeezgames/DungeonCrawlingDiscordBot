Module RunProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        RequireNoTokens(
            tokens,
            RunText,
            builder,
            Sub()
                RequireCharacter(
                    player,
                    builder,
                    Sub(character)
                        RequireInCombat(
                            character,
                            builder,
                            Sub()
                                If player.Run() Then
                                    Dim canvas = DrawPOV(player)
                                    builder.AppendLine($"{character.FullName} runs!
```{canvas.Output}```")
                                    Return
                                End If
                                builder.AppendLine(DoCounterAttacks(character, $"{character.FullName} could not get away.
"))
                            End Sub)
                    End Sub)
            End Sub)
    End Sub
End Module
