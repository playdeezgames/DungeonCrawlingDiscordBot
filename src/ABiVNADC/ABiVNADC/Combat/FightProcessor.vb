Module FightProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        RequireCharacter(
            player,
            builder,
            Sub(character)
                If Not character.InCombat Then
                    builder.AppendLine($"{character.FullName} is not in combat.")
                    Return
                End If
                If Not character.CanFight Then
                    builder.AppendLine($"{character.FullName} needs to recover energy.")
                    Return
                End If
                character.Attack(character.Location.Enemy(character), builder)
                PerformCounterAttacks(character, builder)
            End Sub)
    End Sub

    Friend Sub PerformCounterAttacks(character As Character, builder As StringBuilder)
        If character.InCombat Then
            Dim enemies = character.Location.Enemies(character)
            For Each enemy In enemies
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
            enemy.Attack(character, builder)
        Else
            enemy.CombatRest(builder)
        End If
    End Sub
End Module
