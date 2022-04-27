Module CreateDungeonProcessor
    Friend Function Run(player As Player, tokens As IEnumerable(Of String)) As String
        If tokens.Count < 2 Then
            Return $"The syntax is: `create dungeon (difficulty) (name)`"
        End If
        Dim difficulty = ParseDifficulty(tokens.First)
        If difficulty = Difficulty.None Then
            Return "The valid difficulties are `yermom, easy, normal, difficult, too`."
        End If
        Dim dungeonName = String.Join(" "c, tokens.Skip(1))
        If player.CreateDungeon(dungeonName, difficulty) Then
            Return $"You create {dungeonName}."
        End If
        Return $"Failed to create {dungeonName}!"
    End Function
End Module
