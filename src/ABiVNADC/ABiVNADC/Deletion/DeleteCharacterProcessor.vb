Module DeleteCharacterProcessor
    Friend Function Run(player As Player, tokens As IEnumerable(Of String)) As String
        If Not tokens.Any Then
            Return $"The syntax is: `delete character (name)`"
        End If
        Dim characterName = String.Join(" "c, tokens)
        If player.DeleteCharacter(characterName) Then
            Return $"You delete {characterName}."
        End If
        Return $"Failed to delete {characterName}!"
    End Function
End Module
