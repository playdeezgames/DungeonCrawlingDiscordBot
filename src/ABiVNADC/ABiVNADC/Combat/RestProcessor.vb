Module RestProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        RequireNoTokens(
            tokens,
            RestText,
            builder,
            Sub()
                RequireCharacter(
                    player,
                    builder,
                    Sub(character)
                        If player.InCombat Then
                            builder.AppendLine(HandleCombatRest(player))
                            Return
                        End If
                        builder.AppendLine(HandleNonCombatRest(player))
                    End Sub)
            End Sub)
    End Sub

    Private Function HandleNonCombatRest(player As Player) As String
        Dim character = player.Character
        Return character.NonCombatRest()
    End Function

    Private Function HandleCombatRest(player As Player) As String
        Dim character = player.Character
        Dim restAmount = character.CombatRest()
        Return FightProcessor.DoCounterAttacks(character, $"{character.Name} recovers {restAmount} energy.
")
    End Function
End Module
