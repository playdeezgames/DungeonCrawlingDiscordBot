Module FightProcessor
    Friend Function Run(player As Player, tokens As IEnumerable(Of String)) As String
        If Not player.CanFight Then
            Return "You cannot do that now!"
        End If
        Dim character = player.Character
        Dim enemy = character.Location.Enemy
        Dim builder As New StringBuilder
        Dim attack = character.RollAttack
        builder.AppendLine($"{character.Name} rolls an attack of {attack}!")
        Dim defend = enemy.RollDefend
        builder.AppendLine($"{enemy.Name} rolls a defend of {defend}!")
        Dim damage = If(attack > defend, attack - defend, 0)
        If damage > 0 Then
            enemy.AddWounds(damage)
            builder.AppendLine($"{character.Name} hits!")
            builder.AppendLine($"{enemy.Name} takes {damage} damage!")
        Else
            builder.AppendLine($"{character.Name} misses!")
        End If
        If enemy.IsDead Then
            builder.AppendLine($"{character.Name} kills {enemy.Name} (they had a family, you know!)")
            enemy.Destroy()
        End If
        Return builder.ToString
    End Function
End Module
