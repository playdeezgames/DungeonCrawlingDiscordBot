Module RestProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        If tokens.Any Then
            builder.AppendLine("The command is just `rest`.")
            Return
        End If
        RequireCharacter(
            player,
            builder,
            Function(character)
                If player.InCombat Then
                    Return HandleCombatRest(player)
                End If
                Return HandleNonCombatRest(player)
            End Function)
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
