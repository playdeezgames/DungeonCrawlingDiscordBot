Module FightProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        RequireCharacter(
            player,
            builder,
            Function(character)
                If Not character.InCombat Then
                    Return $"{character.FullName} is not in combat."
                End If
                If Not character.CanFight Then
                    Return $"{character.FullName} needs to recover energy."
                End If
                Return DoCounterAttacks(character, character.Attack(character.Location.Enemy(character)))
            End Function)
    End Sub

    Friend Function DoCounterAttacks(character As Character, output As String) As String
        Dim builder As New StringBuilder
        builder.Append(output)
        FightProcessor.PerformCounterAttacks(character, builder)
        Return builder.ToString
    End Function

    Friend Sub PerformCounterAttacks(character As Character, builder As StringBuilder)
        If character.InCombat Then
            For Each enemy In character.Location.Enemies(character)
                If Not character.Exists Then
                    Continue For
                End If
                PerformCounterAttack(character, builder, enemy)
            Next
        End If
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
