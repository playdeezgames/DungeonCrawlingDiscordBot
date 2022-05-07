Module DeleteDungeonProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        If Not tokens.Any Then
            builder.AppendLine($"The syntax is: `delete dungeon (name)`")
            Return
        End If
        Dim dungeonName = String.Join(" "c, tokens)
        If player.DeleteDungeon(dungeonName) Then
            builder.AppendLine($"You delete {dungeonName}.")
            Return
        End If
        builder.AppendLine($"Failed to delete {dungeonName}!")
        Return
    End Sub
End Module
