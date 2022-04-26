Module CreateCharacterProcessor
    Friend Function Run(player As Player, tokens As IEnumerable(Of String)) As String
        If Not tokens.Any Then
            Return $"The syntax is: `create character (name)`"
        End If
        Dim characterName = String.Join(" "c, tokens)
        If player.CreateCharacter(characterName) Then
            Return $"You create {characterName}."
        End If
        Return $"Failed to create {characterName}!"
    End Function
End Module
