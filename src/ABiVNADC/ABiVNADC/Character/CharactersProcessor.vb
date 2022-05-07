Module CharactersProcessor
    Friend Sub Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String))
        RequireNoTokens(
            tokens,
            CharactersText,
            builder,
            Sub()
                builder.AppendLine("Characters:")
                Dim characters = player.Characters
                If characters.Any Then
                    For Each character In characters
                        builder.AppendLine($"- {character.Name}")
                    Next
                Else
                    builder.AppendLine("(none)")
                End If
            End Sub)
    End Sub
End Module
