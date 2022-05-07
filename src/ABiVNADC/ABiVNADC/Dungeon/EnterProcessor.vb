Module EnterProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        Dim character = player.Character
        If character Is Nothing Then
            builder.AppendLine($"You don't have a currently selected character. You ain't enterin' NOTHING.")
            Return
        End If
        If character.Location IsNot Nothing Then
            builder.AppendLine($"{character.Name} is already in a dungeon.")
            Return
        End If
        Dim dungeonName = String.Join(" "c, tokens)
        Dim dungeon = player.Dungeons.SingleOrDefault(Function(x) x.Name = dungeonName)
        If dungeon Is Nothing Then
            builder.AppendLine($"{dungeonName} doesn't exist!")
            Return
        End If
        character.Location = dungeon.StartingLocation
        Dim canvas = DrawPOV(player)
        builder.AppendLine($"{character.FullName} enters {dungeonName}.
```{canvas.Output}```")
    End Sub
End Module
