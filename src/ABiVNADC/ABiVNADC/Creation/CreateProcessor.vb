Module CreateProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        'TODO: deprecate me
        If tokens.Any Then
            Select Case tokens.First
                Case CharacterText
                    CreateCharacterProcessor.Run(player, builder, tokens.Skip(1))
                    Return
                Case DungeonText
                    CreateDungeonProcessor.Run(player, builder, tokens.Skip(1))
                    Return
            End Select
        End If
        builder.AppendLine($"Create what? CREATE *WHAT*?!?")
    End Sub
End Module
