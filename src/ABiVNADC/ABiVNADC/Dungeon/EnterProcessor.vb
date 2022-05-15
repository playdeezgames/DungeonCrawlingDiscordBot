Module EnterProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        RequireNoTokens(
            tokens,
            EnterText,
            builder,
            Sub()
                RequireCharacter(
                    player,
                    builder,
                    Sub(character)
                        RequireLocation(
                            character,
                            builder,
                            Sub(location)
                                If Not location.HasRoute(Direction.Inward) Then
                                    builder.AppendLine("There is no entrance here.")
                                    Return
                                End If
                                If character.InCombat Then
                                    builder.AppendLine($"{character.FullName} cannot enter while in combat!")
                                    Return
                                End If
                                character.Location = location.Routes(Direction.Inward).ToLocation
                                builder.AppendLine($"{character.FullName} enters.")
                                ShowCurrentLocation(player, builder)
                            End Sub)
                    End Sub)
            End Sub)

    End Sub
End Module
