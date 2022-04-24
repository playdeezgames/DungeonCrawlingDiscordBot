﻿Module RestProcessor
    Friend Function Run(player As Player, tokens As IEnumerable(Of String)) As String
        If tokens.Any Then
            Return "The command is just `rest`."
        End If
        If Not player.HasCharacter Then
            Return "You don't have a current character."
        End If
        If player.InCombat Then
            Return HandleCombatRest(player)
        Else
            Return HandleNonCombatRest(player)
        End If
    End Function

    Private Function HandleNonCombatRest(player As Player) As String
        Dim character = player.Character
        character.NonCombatRest()
        Return $"{character.Name} rests fully."
    End Function

    Private Function HandleCombatRest(player As Player) As String
        Dim character = player.Character
        Dim restAmount = character.CombatRest()
        Dim builder As New StringBuilder
        builder.AppendLine($"{character.Name} recovers {restAmount} energy.")
        FightProcessor.DoCounterAttacks(player, character, builder)
        Return builder.ToString
    End Function
End Module
