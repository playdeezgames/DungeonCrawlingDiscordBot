Module DeleteDungeonProcessor
    Friend Function Run(player As Player, tokens As IEnumerable(Of String)) As String
        If tokens.Any Then
            Dim dungeonName = String.Join(" "c, tokens)
            If player.DeleteDungeon(dungeonName) Then
                Return $"You delete {dungeonName}."
            Else
                Return $"Failed to delete {dungeonName}!"
            End If
        End If
        Return $"The syntax is: `delete dungeon (name)`"
    End Function
End Module
