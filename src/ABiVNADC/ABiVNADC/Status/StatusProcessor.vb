Module StatusProcessor
    Friend Function Run(player As Player, tokens As IEnumerable(Of String)) As String
        If tokens.Any Then
            Return "Round here, we only respond to a raw `status` commmand!"
        Else
            Dim builder As New StringBuilder
            builder.AppendLine("Status:")
            Dim character = player.Character
            If character Is Nothing Then
                builder.AppendLine("You don't have a currently selected character!")
            Else
                builder.AppendLine($"Currently selected character: {character.Name}")
            End If
            Return builder.ToString
        End If
    End Function
End Module
