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
                character.PerformCounterAttacks(builder)
            End Sub)
    End Sub
End Module
