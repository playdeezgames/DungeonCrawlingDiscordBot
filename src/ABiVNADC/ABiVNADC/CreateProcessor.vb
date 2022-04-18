Module CreateProcessor
    Friend Function Run(player As Player, tokens As IEnumerable(Of String)) As String
        If tokens.Any Then
            Select Case tokens.First
                Case CharacterText
                    Return CreateCharacterProcessor.Run(player, tokens.Skip(1))
            End Select
        End If
        Return $"Create what? CREATE *WHAT*?!?"
    End Function
End Module
