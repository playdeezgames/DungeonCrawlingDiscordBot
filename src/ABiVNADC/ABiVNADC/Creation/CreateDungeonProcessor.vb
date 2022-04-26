Module CreateDungeonProcessor
    Friend Function Run(player As Player, tokens As IEnumerable(Of String)) As String
        If Not tokens.Any Then
            Return $"The syntax is: `create dungeon (name)`"
        End If
        Dim dungeonName = String.Join(" "c, tokens)
        If player.CreateDungeon(dungeonName) Then
            Return $"You create {dungeonName}."
        End If
        Return $"Failed to create {dungeonName}!"
    End Function
End Module
