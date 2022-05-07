Module CharactersProcessor
    Friend Function Run(player As Player, builder As StringBuilder, tokens As IEnumerable(Of String)) As String
        If tokens.Any Then
            Return "Round here, just `characters` is the command. I'm picky."
        End If

        builder.AppendLine("Characters:")
        Dim characters = player.Characters
        If characters.Any Then
            For Each character In characters
                builder.AppendLine($"- {character.Name}")
            Next
        Else
            builder.AppendLine("(none)")
        End If
        Return builder.ToString
    End Function
End Module
