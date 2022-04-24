Module FightProcessor
    Friend Function Run(player As Player, tokens As IEnumerable(Of String)) As String
        If Not player.CanFight Then
            Return "You cannot do that now!"
        End If
        Dim character = player.Character
        Return character.Attack(character.Location.Enemy)
    End Function
End Module
