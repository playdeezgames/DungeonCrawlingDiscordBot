Module EnterProcessor
    Friend Function Run(player As Player, tokens As IEnumerable(Of String)) As String
        Dim character = player.Character
        If character IsNot Nothing Then
            Dim dungeonName = String.Join(" "c, tokens)
            Dim dungeon = player.Dungeons.SingleOrDefault(Function(x) x.Name = dungeonName)
            If dungeon IsNot Nothing Then
                character.Location = dungeon.StartingLocation
                Return $"{character.Name} enters {dungeonName}."
            End If
            Return $"{dungeonName} doesn't exist!"
        End If
        Return $"You don't have a currently selected character. You ain't enterin' NOTHING."
    End Function
End Module
