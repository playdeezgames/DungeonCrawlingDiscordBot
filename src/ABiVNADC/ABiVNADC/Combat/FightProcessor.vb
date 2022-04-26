Module FightProcessor
    Friend Function Run(player As Player, tokens As IEnumerable(Of String)) As String
        If Not player.HasCharacter Then
            Return "You have no current character."
        End If
        If Not player.InCombat Then
            Return $"{player.Character.FullName} is not in combat."
        End If
        If Not player.CanFight Then
            Return $"{player.Character.FullName} needs to recover energy."
        End If
        Dim character = player.Character
        Dim builder As New StringBuilder
        builder.Append(character.Attack(character.Location.Enemy(character)))
        DoCounterAttacks(player, character, builder)
        Return builder.ToString
    End Function

    Friend Sub DoCounterAttacks(player As Player, character As Character, builder As StringBuilder)
        If Not character.HasLocation Then
            Return
        End If
        For Each enemy In character.Location.Enemies(character)
            If Not player.HasCharacter Then
                Continue For
            End If

            PerformCounterAttack(character, builder, enemy)
        Next
    End Sub

    Private Sub PerformCounterAttack(character As Character, builder As StringBuilder, enemy As Character)
        builder.AppendLine("---")
        If enemy.CanFight Then
            builder.Append(enemy.Attack(character))
        Else
            Dim restAmount = enemy.CombatRest
            builder.AppendLine($"{enemy.Name} recovers {restAmount} energy.")
        End If
    End Sub
End Module
