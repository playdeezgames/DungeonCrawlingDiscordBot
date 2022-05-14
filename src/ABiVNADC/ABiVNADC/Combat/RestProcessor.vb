Module RestProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        RequireNoTokens(
            tokens,
            RestText,
            builder,
            Sub()
                RequireCharacterLocation(
                    player,
                    builder,
                    Sub(character, location)
                        If Not location.LocationType.CanRest Then
                            builder.AppendLine($"{character.FullName} cannot rest here.")
                            Return
                        End If
                        If player.InCombat Then
                            HandleCombatRest(player, builder)
                            Return
                        End If
                        HandleNonCombatRest(player, builder)
                    End Sub)
            End Sub)
    End Sub

    Private Sub HandleNonCombatRest(player As Player, builder As StringBuilder)
        Dim character = player.Character
        character.NonCombatRest(builder)
    End Sub

    Private Sub HandleCombatRest(player As Player, builder As StringBuilder)
        player.CombatRest(builder)
    End Sub
End Module
