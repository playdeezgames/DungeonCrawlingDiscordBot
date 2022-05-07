Module DeleteProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        If tokens.Any Then
            Select Case tokens.First
                Case CharacterText
                    DeleteCharacterProcessor.Run(player, builder, tokens.Skip(1))
                    Return
                Case DungeonText
                    DeleteDungeonProcessor.Run(player, builder, tokens.Skip(1))
                    Return
            End Select
        End If
        builder.AppendLine($"Delete what? DELETE *WHAT*?!?")
    End Sub
End Module
