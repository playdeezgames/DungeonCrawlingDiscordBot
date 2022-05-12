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
                                If Not player.Run(builder) Then
                                    ShowCurrentLocation(player, builder)
                                End If
                            End Sub)
                    End Sub)
            End Sub)
    End Sub
End Module
