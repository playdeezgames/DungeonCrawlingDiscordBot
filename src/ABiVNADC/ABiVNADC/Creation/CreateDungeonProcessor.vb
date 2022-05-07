Module CreateDungeonProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        'TODO: special refactor... or just wait to deprecate!
        If tokens.Count < 2 Then
            builder.AppendLine($"The syntax is: `create dungeon (difficulty) (name)`")
            Return
        End If
        Dim difficulty = ParseDifficulty(tokens.First)
        If difficulty = Difficulty.None Then
            builder.AppendLine("The valid difficulties are `yermom, easy, normal, difficult, too`.")
            Return
        End If
        Dim dungeonName = String.Join(" "c, tokens.Skip(1))
        If player.CreateDungeon(dungeonName, difficulty) Then
            builder.AppendLine($"You create {dungeonName}.")
            Return
        End If
        builder.AppendLine($"Failed to create {dungeonName}!")
    End Sub
End Module
