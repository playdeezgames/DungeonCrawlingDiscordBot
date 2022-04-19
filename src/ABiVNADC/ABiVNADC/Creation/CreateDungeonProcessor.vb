Module CreateDungeonProcessor
    Friend Function Run(player As Player, tokens As IEnumerable(Of String)) As String
        If tokens.Any Then
            Dim dungeonName = String.Join(" "c, tokens)
            If player.CreateDungeon(dungeonName) Then
                Return $"You create {dungeonName}."
            Else
                Return $"Failed to create {dungeonName}!"
            End If
        End If
        Return $"The syntax is: `create dungeon (name)`"
    End Function
End Module
