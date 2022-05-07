Module SwitchProcessor
    Friend Function Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String)) As String
        If tokens.Any Then
            Select Case tokens.First.ToLower
                Case CharacterText
                    Return HandleSwitchCharacter(player, tokens.Skip(1))
            End Select
        End If
        Return "I only understand `switch character (name)`."
    End Function

    Private Function HandleSwitchCharacter(player As Player, tokens As IEnumerable(Of String)) As String
        If tokens.Any Then
            Dim characterName = String.Join(" "c, tokens)
            If player.SwitchCharacter(characterName) Then
                Return $"You switch to {characterName}."
            End If
            Return $"You fail to switch to {characterName}."
        End If
        Return "I only understand `switch character (name)`."
    End Function
End Module
