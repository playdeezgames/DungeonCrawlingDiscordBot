Module DeleteDungeonProcessor
    Friend Function Run(player As Player, tokens As IEnumerable(Of String)) As String
        If Not tokens.Any Then
            Return $"The syntax is: `delete dungeon (name)`"
        End If
        Dim dungeonName = String.Join(" "c, tokens)
        If player.DeleteDungeon(dungeonName) Then
            Return $"You delete {dungeonName}."
        End If
        Return $"Failed to delete {dungeonName}!"
    End Function
End Module
