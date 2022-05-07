Module DeleteProcessor
    Friend Function Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String)) As String
        If tokens.Any Then
            Select Case tokens.First
                Case CharacterText
                    Return DeleteCharacterProcessor.Run(player, tokens.Skip(1))
                Case DungeonText
                    Return DeleteDungeonProcessor.Run(player, tokens.Skip(1))
            End Select
        End If
        Return $"Delete what? DELETE *WHAT*?!?"
    End Function
End Module
