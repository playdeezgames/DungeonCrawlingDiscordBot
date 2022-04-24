Module FightProcessor
    Friend Function Run(player As Player, tokens As IEnumerable(Of String)) As String
        If Not player.CanFight Then
            Return "You cannot do that now!"
        End If
        Dim character = player.Character
        Dim builder As New StringBuilder
        builder.Append(character.Attack(character.Location.Enemy))
        For Each enemy In character.Location.Enemies
            If player.HasCharacter Then
                builder.AppendLine("---")
                builder.Append(enemy.Attack(character))
            End If
        Next
        Return builder.ToString
    End Function
End Module
