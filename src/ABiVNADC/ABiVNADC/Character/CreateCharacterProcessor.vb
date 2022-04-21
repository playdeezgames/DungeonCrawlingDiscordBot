Module CreateCharacterProcessor
    Friend Function Run(player As Player, tokens As IEnumerable(Of String)) As String
        If tokens.Any Then
            Dim characterName = String.Join(" "c, tokens)
            If player.CreateCharacter(characterName) Then
                Return $"You create {characterName}."
            Else
                Return $"Failed to create {characterName}!"
            End If
        End If
        Return $"The syntax is: `create character (name)`"
    End Function
End Module
