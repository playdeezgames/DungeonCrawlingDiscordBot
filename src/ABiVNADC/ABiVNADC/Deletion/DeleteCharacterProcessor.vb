Module DeleteCharacterProcessor
    Friend Function Run(player As Player, tokens As IEnumerable(Of String)) As String
        If tokens.Any Then
            Dim characterName = String.Join(" "c, tokens)
            If player.DeleteCharacter(characterName) Then
                Return $"You delete {characterName}."
            Else
                Return $"Failed to delete {characterName}!"
            End If
        End If
        Return $"The syntax is: `delete character (name)`"
    End Function
End Module
