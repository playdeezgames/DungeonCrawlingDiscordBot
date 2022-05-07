Module EnterProcessor
    Friend Function Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String)) As String
        Dim character = player.Character
        If character Is Nothing Then
            Return $"You don't have a currently selected character. You ain't enterin' NOTHING."
        End If
        If character.Location IsNot Nothing Then
            Return $"{character.Name} is already in a dungeon."
        End If
        Dim dungeonName = String.Join(" "c, tokens)
        Dim dungeon = player.Dungeons.SingleOrDefault(Function(x) x.Name = dungeonName)
        If dungeon Is Nothing Then
            Return $"{dungeonName} doesn't exist!"
        End If
        character.Location = dungeon.StartingLocation
        Dim canvas = DrawPOV(player)
        Return $"{character.FullName} enters {dungeonName}.
```{canvas.Output}```"
    End Function
End Module
