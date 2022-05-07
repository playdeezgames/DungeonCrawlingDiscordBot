Module RestProcessor
    Friend Function Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String)) As String
        If tokens.Any Then
            Return "The command is just `rest`."
        End If
        Return RequireCharacter(
            player,
            Function(character)
                If player.InCombat Then
                    Return HandleCombatRest(player)
                End If
                Return HandleNonCombatRest(player)
            End Function)
    End Function

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
